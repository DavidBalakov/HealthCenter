using HealthCenter.Models;
using Microsoft.AspNetCore.Identity;

interface IPatientsRepository
{
    Task<User> FindByNameAsync(string name);
    Task<IdentityResult> CreateAsync(User user, string password);
    Task AddToRoleAsync(User user, string role);
    Task SignInAsync(User user);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<SignInResult> PasswordSignInAsync(User user, string password);
    Task SignOutAsync();
}