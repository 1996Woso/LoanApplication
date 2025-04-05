using LoanApplictationApp.Models.Domain;
using LoanApplictationApp.Models.DTO.Application;

namespace LoanApplictationApp.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDTO>> GetApplicationsAsync(Guid? applicantId, long? loanProcessorNo);
        Task<Application> AddApplicationAsync(AddApplicationDTO application, string applicantId);
    }
}
