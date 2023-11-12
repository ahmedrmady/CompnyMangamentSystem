using Demo.DAL.Data.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using AutoMapper;
using Demo.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class AccountController : Controller
	{
		//private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(/*IMapper mapper,*/UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			//_mapper = mapper;
			this._userManager = userManager;
			_signInManager = signInManager;
		}
		#region Register

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is null)
				{
					user = new ApplicationUser()
					{
						FName = model.FName,
						Email = model.Email,
						IsAgree = model.IsAgree,
						UserName = model.Email.Split('@')[0]

					};

					var result = await _userManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
						return RedirectToAction(nameof(Login));

					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);

				}

				ModelState.AddModelError(string.Empty, "The user is already existe");

			}
			return View(model);
		} 
		#endregion


		#region Login

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{

				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{

					var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
					if (result.Succeeded)
						RedirectToAction(nameof(HomeController.Index), "Home");



				}

			}

			return View();
		}
		#endregion


		#region Sign Out

		public  new async Task<IActionResult> SignOut ()
		{
			await  _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion

		#region RestPassword

		[HttpGet]
		public IActionResult ForgetPassword()
		{

			return View();
		}

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
			if(ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if(user is not null)
				{
					var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
					var ResetPassword = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = Token },Request.Scheme);

					var email = new Email()
					{
						Subject = "Reset PAssword",
						Recipients = model.Email,
						Body = ResetPassword

					};

					EmailSettings.SendMailAsync(email);
					return RedirectToAction(nameof(CheckYourMail));


				}

				ModelState.AddModelError(string.Empty, "The Email is not found");

			}

            return View(model);
        }

		[HttpGet]
		public IActionResult CheckYourMail()
		{
			return View();
		}

		[HttpGet]
		public IActionResult RestPassword(string Email , string Token)
		{
			TempData["Email"] = Email;
			TempData["Token"] = Token;


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RestPassword(ResetPasswordModelView model)
		{

			 var email = TempData["Email"] as string;
			var token = TempData["Token"] as string;
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(email);
				if(user is not null)
				{
					var Result =await _userManager.ResetPasswordAsync(user,token,model.NewPassword);
					if (Result.Succeeded)
						RedirectToAction(nameof(Login));

					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);

				}

			}


			return View(model);
		}

		#endregion


	}
}
