using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplicationApp.API.Repositories
{
    public interface IApplicationRepository
    {
        Task<Application> CreatAsync(Application application, string applicantId);
        Task<Application> UpdateAsync(long id, Application application);
        Task<IEnumerable<Application>> GetAllAsync(string? ApplicantId, long? LoanProcesorNo);
        Task<Application> GetByIdAsync(long id);
        
      
    }
}
