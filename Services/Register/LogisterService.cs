using HealthCenter.Models;
using HealthCenter.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HealthCenter.Services.Register
{
    class LogisterService : ILogisterService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LogisterService(UserManager<User> userManager
            , SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> Register(RegisterViewModel registerRequest)
        {
            User user = new User()
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager
                    .SignInAsync(user, isPersistent: false);
            }
            return result;
        }

        public async Task<SignInResult> LogIn(LogInViewModel logInViewModel)
        {
            User user = await _userManager
                    .FindByNameAsync(logInViewModel.UserName);
            if (user != null)
            {
                var passwordCheck = await _userManager
                    .CheckPasswordAsync(user
                    , logInViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager
                        .PasswordSignInAsync(user
                        , logInViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return result;
                    }
                }
            }
            return SignInResult.Failed;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}