using LoanApplictationApp.Data;

namespace LoanApplictationApp.Services
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetCurrentUserAsync();
    }
}
