using LoanApplictationApp.Models;
using LoanApplictationApp.Models.DTO;

namespace LoanApplictationApp.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDTO>> GetApplicationsAsync();
        Task<Application> AddApplicationAsync(AddApplicationDTO application, string applicantId);
    }
}
