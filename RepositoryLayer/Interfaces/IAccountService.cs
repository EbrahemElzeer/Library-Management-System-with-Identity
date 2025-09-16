using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOS;
using ApplicationCore.Entity;
using Library_Managment_System.Models;
using Microsoft.AspNetCore.Identity;


namespace ApplicationCore.Interfaces
{
    public interface IAccountService
    {

        Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
        Task<Result> LoginAsync(LoginDto dto);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

    }
}
