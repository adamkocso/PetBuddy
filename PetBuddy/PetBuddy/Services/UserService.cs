using Microsoft.AspNetCore.Identity;
using PetBuddy.Models;
using PetBuddy.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetBuddy.ViewModels;

namespace PetBuddy.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext applicationContext;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserService(ApplicationContext applicationContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.applicationContext = applicationContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<List<string>> LoginAsync(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                model.ErrorMessages.Add("User with the given Email does not exist");
                return model.ErrorMessages;
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false,
                lockoutOnFailure: false);
            model.ErrorMessages = CheckLoginErrors(result, model.ErrorMessages);
            return model.ErrorMessages;
        }

        private static List<string> CheckLoginErrors(SignInResult result, List<string> errors)
        {
            if (!result.Succeeded)
            {
                errors.Add("Invalid login attempt");
            }

            return errors;
        }
        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new User {UserName = model.Username, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await AddUserToRoleAsync(user);
                return result;
            }

            return result;
        }

        public async Task SaveUserSettings(EditProfileViewModel editProfileViewModel, string userId)
        {
            if (editProfileViewModel.City != null && editProfileViewModel.Name != null )
            {
                var user = await applicationContext.Users.SingleOrDefaultAsync(p => p.Id == userId);
                user.City = editProfileViewModel.City;
                user.UserName = editProfileViewModel.Name;
                applicationContext.Users.Update(user);
                await applicationContext.SaveChangesAsync();
            }
        }

        public async Task AddUserToRoleAsync(User user)
        {
            await userManager.AddToRoleAsync(user, "Guest");

        }
    }
}