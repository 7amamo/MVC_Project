using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo.presentaton.Layer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        public  bool CheckEmailExists(string email)
        {
            return _userManager.FindByEmailAsync(email).Result is not null; 
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (CheckEmailExists(model.Email))
            {
                ModelState.AddModelError(string.Empty, "Email is Already in Use");

            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
                return RedirectToAction(nameof(Login));

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View();


        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user =await _userManager.FindByEmailAsync(model.Email);
            if(user is not null)
            {
                if (_userManager.CheckPasswordAsync(user,model.Password).Result)
                {
                    var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe , false).Result;

                    if (result.Succeeded) return RedirectToAction(nameof(HomeController.Index),
                            nameof(HomeController).Replace("Controller",string.Empty));
                }
            }
            ModelState.AddModelError(string.Empty, "InCorrect Email Or Password");
            return View();
        }

        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

		public new IActionResult ForgetPassword()
		{
			return View();
		}
        [HttpPost]
		public  IActionResult ForgetPassword( ForgetPasswordViewModel model)
		{
			if (!ModelState.IsValid) return View(model);
			var user = _userManager.FindByEmailAsync(model.Email).Result;

            if (user is not null)
            {
                var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;

                var url = Url.Action("ResetPassword", "Account",new {email = model.Email , Token = token} , Request.Scheme );

                var email = new EMail()
                {
                    Subject = "Reset Passeord",
                    Body = url!,
                    To = model.Email 
                };

                MailSettings.SendEmail(email);

                return RedirectToAction(nameof(CheckYourInBox));
            }
            ModelState.AddModelError(string.Empty, "User Not Found");
            return View(model);
		}

        public IActionResult CheckYourInBox () 
        {
            return View();
        }

		public IActionResult ResetPassword(string email , string token)
		{
            if (email is null || token is null) return BadRequest();
            TempData["Email"] = email;
            TempData["Token"] = token;
			return View();
		}

        [HttpPost]
		public IActionResult ResetPassword(ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

            var  Token = TempData["Token"]?.ToString() ??string.Empty;
			var Email = TempData["Email"]?.ToString() ?? string.Empty;

            var user = _userManager.FindByEmailAsync(Email).Result;

            if (user != null)
            {
				var result = _userManager.ResetPasswordAsync(user, Token, model.Password).Result;
                if (result.Succeeded) return RedirectToAction(nameof(Login));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
			}
            ModelState.AddModelError(string.Empty, "User Not Found");
            return View();

		}
	}
}


