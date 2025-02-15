﻿using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Countries;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Groups;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTeacherRepository _groupTeacherRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository,
                            IMapper mapper,
                            IGroupTeacherRepository groupTeacherRepository,
                            AppDbContext context)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _groupTeacherRepository = groupTeacherRepository;
            _context = context;
        }

        public async Task CreateAsync(GroupCreateDto model)
        {
            var data = _mapper.Map<Group>(model);

            await _groupRepository.CreateAsync(data);
            await _groupTeacherRepository.CreateAsync(new GroupTeachers { GroupId = data.Id, TeacherId = model.teacherId });
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var group = await _groupRepository.GetById((int)id);

            if (group is null) throw new NotFoundException("Group not found");

            await _groupRepository.DeleteAsync(_mapper.Map<Group>(group));
        }

        public async Task EditAsync(int? id, GroupEditDto model)
        {
            if (id is null) throw new ArgumentNullException();

            var group = await _groupRepository.GetById((int)id);

            if (group is null) throw new NotFoundException("Group not found");

            await _groupRepository.EditAsync(_mapper.Map(model,group));
        }

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            var datas = await _groupRepository.FindAllWithIncludes()
                                              .Include(m => m.Education).Include(m => m.Room)
                                              .Include(m => m.GroupTeachers)
                                              .ThenInclude(m => m.Teacher)
                                              .Include(m => m.GroupStudents)
                                              .ThenInclude(m => m.Student).ToListAsync();

            return _mapper.Map<IEnumerable<GroupDto>>(datas);
        }

        public async Task<GroupDto> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var groupEntity = await _context.Groups.FindAsync(id);

            if (groupEntity == null)
            {
                return null;
            }

            var groupDto = _mapper.Map<GroupDto>(groupEntity);

            return groupDto;
        }
    }
}
