﻿using Service.DTOs.Admin.Countries;
using Service.DTOs.Admin.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Task CreateAsync(GroupCreateDto model);
        Task EditAsync(int? id, GroupEditDto model);
        Task<GroupDto> GetByIdAsync(int? id);
        Task DeleteAsync(int? id);
        Task<IEnumerable<GroupDto>> GetAllAsync();

    }
}
