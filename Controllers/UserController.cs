using HealthCenter.Models.ViewModels;
using HealthCenter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HealthCenter.Models;
using HealthCenter.Models.ViewModels;

namespace Products.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(UserManager<User> userManager
            , SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerRequest)
        {
            if (ModelState.IsValid)
            {

                User user = new User()
                {
                    UserName = registerRequest.UserName,
                    Email = registerRequest.Email,
                };

                var result = await _userManager.CreateAsync(user
                    , registerRequest.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager
                        .SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                TempData["ErrorRegister"] = "Wrong Password";
                return View(registerRequest);
            }
            TempData["ErrorRegister"] = "Please fill out all the fields";
            return View(registerRequest);
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInRequest)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager
                    .FindByNameAsync(logInRequest.UserName);
                if (user != null)
                {
                    var passwordCheck = await _userManager
                        .CheckPasswordAsync(user
                        , logInRequest.Password);
                    if (passwordCheck)
                    {
                        var result = await _signInManager
                            .PasswordSignInAsync(user
                            , logInRequest.Password, false, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View(logInRequest);
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
