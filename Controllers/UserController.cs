using HealthCenter.Models.ViewModels;
using HealthCenter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HealthCenter.Models;
using HealthCenter.Models.ViewModels;
using HealthCenter.Services.Register;

namespace Products.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogisterService _logisterService;
        public UserController(ILogisterService logisterService)
        {
            this._logisterService = logisterService;
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

                IdentityResult result = await _logisterService.Register(registerRequest);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
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
                var result = await _logisterService.LogIn(logInRequest);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(logInRequest);
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _logisterService.LogOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
