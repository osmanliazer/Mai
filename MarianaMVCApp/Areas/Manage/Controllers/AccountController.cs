using MarianaMVCApp.Areas.Manage.ViewModels;
using MarianaMVCApp.Models;
using MarianaMVCApp.Utilites.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarianaMVCApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            AppUser user = new AppUser
            {
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                Name = registerVm.Name,
                Surname = registerVm.SurName
            };
            var result =await _userManager.CreateAsync(user,registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);

                }
                return View(registerVm);
            }
            await _userManager.AddToRoleAsync(user,UserRoles.Admin.ToString());
            await _signInManager.SignInAsync(user, true);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public IActionResult Login()
        {
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if(!ModelState.IsValid) return View(loginVm);
            AppUser user = await _userManager.FindByNameAsync(loginVm.UserNameOrEail);
            if(user == null)
            {
                user= await _userManager.FindByEmailAsync(loginVm.UserNameOrEail);
                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "UserName Email or password was wrong");
                    return View(loginVm);
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.IsRemembered, true);
            if(!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "UserName Email or password was wrong");
                return View(loginVm);
            }
            if(result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "You Locked please try some minute later");
                return View(loginVm);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name=item.ToString()});
                }
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }
    }
}
