using EF_layer.Model;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.DTOS;
using ApplicationCore.Entity;
using Library_Managment_System.Services;
using System.Net;
using System;

namespace ApplicationCore.DomainServices
{
    public class AccountService:IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailAlreadyExists",
                    Description = "Email is already in use."
                });
            }

            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };
            return await _userManager.CreateAsync(user,registerDto.Password);
        }
        public async Task<Result> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Name);

            if (user == null)
                return Result.Fail("Username or password is incorrect.");

            var isValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isValid)
                return Result.Fail("Username or password is incorrect.");

            var isSuperAdmin = await _userManager.IsInRoleAsync(user, "SuperAdmin");
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (!isSuperAdmin && !isAdmin)
                return Result.Fail("You are not authorized to log in.");

            await _signInManager.SignInAsync(user, dto.RememberMe);
            return Result.Ok();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByNameAsync(changePasswordDto.UserName);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User Not Found." });
             var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                return removeResult;
            var addResult = await _userManager.AddPasswordAsync(user, changePasswordDto.NewPassword);
                return addResult;

        }
        public async Task  ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = WebUtility.UrlEncode(token);

                var resetLink = $"{forgotPasswordDto.CallbackUrlBase}/Account/ResetPassword?token={encodedToken}&email={user.Email}";

                var subject = "Password Reset";
                var body = $"<p>Click the link below to reset your password:</p><a href='{resetLink}'>Reset Password</a>";

                await _emailService.SendEmailAsync(user.Email, subject, body);
            }

        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return IdentityResult.Success;
            }
            var decodedToken = WebUtility.UrlDecode(resetPasswordDto.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDto.NewPassword);
            return result;
        }
    }
}

