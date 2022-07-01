using eStartap.Models;
using eStartap.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eStartap.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser
            {
                UserName = register.Username,
                Email = register.Email,
                Name = register.Name,
                Surname = register.Surname,
            };

            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return View();
            if (login.RememberMe)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", " 5 deqiqelik Blok OLunmusuz");
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username ve ya Password yanlisdir");
                        return View();
                    }
                }
               
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", " 5 deqiqelik Blok OLunmusuz");
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username ve ya Password yanlisdir");
                        return View();
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        
    }
}
