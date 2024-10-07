using HealthCenter.Data;
using HealthCenter.Models;
using Microsoft.AspNetCore.Identity;

class PatientsRepository : IPatientsRepository
{
    private readonly PatientsDbContext _patientsContext;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public PatientsRepository(PatientsDbContext _patientsContext, UserManager<User> userManager,
 SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        this._patientsContext = _patientsContext;
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._roleManager = roleManager;
    }

    public async Task AddToRoleAsync(User user, string role)
    {
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> CreateAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<User> FindByNameAsync(string name)
    {
        return await _userManager.FindByNameAsync(name);
    }

    public async Task<SignInResult> PasswordSignInAsync(User user, string password)
    {
        return await _signInManager.PasswordSignInAsync(user, password, false, false);

    }
    public async Task SaveChangesAsync()
    {
        await _patientsContext.SaveChangesAsync();
    }

    public async Task SignInAsync(User user)
    {
        await _signInManager.SignInAsync(user, isPersistent: false);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public Task SignoutAsync()
    {
        throw new NotImplementedException();
    }

    async Task<User> IPatientsRepository.FindByNameAsync(string name)
    {
        return await _userManager.FindByNameAsync(name);
    }
}