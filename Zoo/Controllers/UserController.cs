using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace Zoo.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new SignUpModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpModel user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser IDuser = new IdentityUser
                {
                    UserName = user.Username,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
                var createResult = await _userManager.CreateAsync(IDuser, user.Password);

                if (createResult.Succeeded)
                {
                    var signUpResult = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);
                    if (signUpResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Login();
                }
            }
            return View();
        }
    }
}
