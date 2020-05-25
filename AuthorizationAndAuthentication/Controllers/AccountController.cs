using System.Threading.Tasks;
using AuthorizationAndAuthentication.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAndAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            var confirmation = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()   
        {
            await _signInManager.SignOutAsync();
            //return View();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var returnUrl = HttpContext.Request.Query["page"].ToString();

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password,
                loginViewModel.RememberMe, true);

            if (result.Succeeded && !string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToRoute(returnUrl);
            }

            if (result.Succeeded && string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
