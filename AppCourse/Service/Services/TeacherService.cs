﻿using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Teachers;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepo = teacherRepository;
            _mapper = mapper;
            
        }


        public async Task Create(TeacherCreateDto model)
        {
            await _teacherRepo.CreateAsync(_mapper.Map<Teacher>(model));
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var teacher = await _teacherRepo.GetById((int)id);

            if (teacher is null) throw new NotFoundException("Teacher not found");

            await _teacherRepo.DeleteAsync(_mapper.Map<Teacher>(teacher));
        }

        public async Task EditAsync(int? id, TeacherEditDto model)
        {

            if (id is null) throw new ArgumentNullException();

            var teacher = await _teacherRepo.GetById((int)id);

            if (teacher is null) throw new NotFoundException("Teacher not found");

            await _teacherRepo.EditAsync(_mapper.Map(model, teacher));
        }

        public async  Task<IEnumerable<TeacherDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<TeacherDto>>(await _teacherRepo.GetAllAsync());

        }

        public async Task<TeacherDto> GetById(int id)
        {
            return _mapper.Map<TeacherDto>(await _teacherRepo.GetById(id));
        }

        public async Task<IEnumerable<TeacherDto>> SearchAsync(string name)
        {
            return _mapper.Map<IEnumerable<TeacherDto>>(await _teacherRepo.FindAll(m => m.Name.Contains(name)));
        }
    }
}
