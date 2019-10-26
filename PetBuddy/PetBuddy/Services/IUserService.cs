﻿using Microsoft.AspNetCore.Identity;
using PetBuddy.Models;
using PetBuddy.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.Services
{
    public interface IUserService
    {
        Task<List<string>> LoginAsync(LoginViewModel model);
        Task Logout();
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task AddUserToRoleAsync(User user);
        Task<User> FindByIdAsync(string userId);
    }
}
