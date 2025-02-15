﻿using Service.DTOs.Account;
using Service.Helpers.Acount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResponse> SignUpAsync(RegisterDto model);
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserDto>GetUserByUsernameAsync(string username);
    }
}
