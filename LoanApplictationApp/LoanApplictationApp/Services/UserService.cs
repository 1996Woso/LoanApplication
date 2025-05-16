using LoanApplictationApp.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LoanApplictationApp.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(
            AuthenticationStateProvider authenticationStateProvider
            ,UserManager<ApplicationUser> userManager
            )
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.userManager = userManager;
        }
        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {

            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if(user.Identity?.IsAuthenticated == true)
            {
                return await userManager.GetUserAsync(user);
            }

            return null;
        }
    }
}
