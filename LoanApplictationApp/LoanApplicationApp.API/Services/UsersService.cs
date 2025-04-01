using System.Security.Claims;
using LoanApplicationApp.API.Data;
using LoanApplictationApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationApp.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IdentityContext identityContext;

        public UsersService(UserManager<ApplicationUser> userManager
            ,IdentityContext identityContext)
        {
            this.userManager = userManager;
            this.identityContext = identityContext;
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(string id)
        {
            return await identityContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IdentityUser> GetIdentityUserAsync(ClaimsPrincipal User)
        {
            return await userManager.GetUserAsync(User);
        }
        public async Task<ApplicationUser> GetApplicationUserAsync(ClaimsPrincipal User)
        {
            return await userManager.GetUserAsync(User);
        }
    }
}
