using HealthCenter.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HealthCenter.Services.Register
{
    public interface ILogisterService
    {
        Task<IdentityResult> Register(RegisterViewModel registerRequest);
        Task<SignInResult> LogIn(LogInViewModel logInViewModel);
        Task LogOut();
    }
}