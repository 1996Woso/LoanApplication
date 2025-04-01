using System.Security.Claims;
using LoanApplictationApp.API.Models;
using Microsoft.AspNetCore.Identity;

namespace LoanApplicationApp.API.Services
{
    public interface IUsersService
    {
        Task<IdentityUser> GetIdentityUserAsync(ClaimsPrincipal User);
        Task<ApplicationUser> GetApplicationUserAsync(ClaimsPrincipal User);
        Task<ApplicationUser> GetApplicationUserByIdAsync(string id);

    }
}
