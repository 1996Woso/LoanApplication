using LoanApplictationApp.Models;
using LoanApplictationApp.Models.DTO.Application;

namespace LoanApplictationApp.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDTO>> GetApplicationsAsync();
        Task<Application> AddApplicationAsync(AddApplicationDTO application, string applicantId);
    }
}
