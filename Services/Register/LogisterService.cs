using HealthCenter.Models;
using HealthCenter.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HealthCenter.Services.Register
{
    class LogisterService : ILogisterService
    {
        private readonly IPatientsRepository _userRepository;
        public LogisterService(IPatientsRepository patientsRepository)
        {
            _userRepository = patientsRepository;
        }
        public async Task<IdentityResult> Register(RegisterViewModel registerRequest)
        {
            User user = new User()
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
            };

            var result = await _userRepository.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                await _userRepository.AddToRoleAsync(user, "User");
                await _userRepository
                    .SignInAsync(user);
            }
            return result;
        }

        public async Task<SignInResult> LogIn(LogInViewModel logInViewModel)
        {
            User user = await _userRepository
                    .FindByNameAsync(logInViewModel.UserName);
            if (user != null)
            {
                var passwordCheck = await _userRepository
                    .CheckPasswordAsync(user
                    , logInViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _userRepository
                        .PasswordSignInAsync(user
                        , logInViewModel.Password);
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
            await _userRepository.SignOutAsync();
        }
    }
}