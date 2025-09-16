using System.Net;
using ApplicationCore.DTOS;
using ApplicationCore.Interfaces;
using AutoMapper;
using EF_layer.Model;
using Library_Managment_System.View_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Library_Managment_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
      

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,IEmailService emailService
            , IMapper mapper, IAccountService accountService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this.mapper = mapper;
            this.accountService = accountService;
        }
       

        [HttpGet]
        [Authorize(Roles="SuperAdmin")]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                 var dto = mapper.Map<RegisterDto>(registerViewModel);
                var result = await accountService.RegisterUserAsync(dto);
                if (result.Succeeded)
                {
                    return RedirectToAction("Register");

                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View("Register",registerViewModel);
        }
      
        public IActionResult logIn()
        {
            return View("logIn");
        }
        [HttpPost]
        public async Task<IActionResult> SavelogIn(LoginUserViewModel loginUserViewModel)
        {
            if (!ModelState.IsValid)
                return View("logIn", loginUserViewModel);

            var dto = mapper.Map<LoginDto>(loginUserViewModel);
            var result = await accountService.LoginAsync(dto);

            if (result.Success)
            {
                return RedirectToAction("Index", "Book");
            }

            ModelState.AddModelError("", result.ErrorMessage ?? "Login failed");
            return View("logIn", loginUserViewModel);
        }


        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
           return RedirectToAction("index","Borrow");

        }

        public IActionResult ChangePassword(string UserName)
        {

            if (UserName == null)
            {
                TempData["ErrorMessage"] = "UserName Is Requird .";
                return RedirectToAction("logIn");
            }
            
            var Model = new ChangePasswordViewModel
            {
                UserName = UserName
            };
            return View("ChangePassword", Model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = mapper.Map<ChangePasswordDto>(changePasswordViewModel);

                var result= await accountService.ChangePasswordAsync(dto);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Book");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("ChangePassword", changePasswordViewModel);           
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new ForgotPasswordDto(model.Email, $"{Request.Scheme}://{Request.Host}");

                await accountService.ForgotPassword(dto);

                TempData["Message"] = "If the email exists, a reset link has been sent to your email.";
                return RedirectToAction("LogIn");

               
            }

            return View(model);
        }



        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {

                TempData["ErorrMessage"] = "Invalid password reset link.";
                return RedirectToAction("logIn");
            }

            var decodedToken=WebUtility.UrlDecode(token);
            var model = new ResetPasswordViewModel
            {

                Token = decodedToken,
                Email = email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var dto = mapper.Map<ResetPasswordDto>(model);
            var result = await accountService.ResetPasswordAsync(dto);

            if (result.Succeeded)
            {
                TempData["Message"] = "Password reset successfully. You can now log in.";
                return RedirectToAction("LogIn");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("ResetPassword", model);
        }
    }
}
