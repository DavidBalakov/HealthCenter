using HealthCenter.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HealthCenter.Services.Register;
using Microsoft.AspNetCore.Authorization;

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
                    TempData["SuccessLogin"] = "You have registered succesfully";
                    return RedirectToAction("Index", "Home");
                }
                TempData["FailedLogin"] = "There was a problem";
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
                    TempData["SuccessLogin"] = "You have logged in succesfully";
                    return RedirectToAction("Index", "Home");
                }
                TempData["FailedLogin"] = "There was a problem";
            }
            return View(logInRequest);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            TempData["SuccessLogin"] = "You are logged out.";
            await _logisterService.LogOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
