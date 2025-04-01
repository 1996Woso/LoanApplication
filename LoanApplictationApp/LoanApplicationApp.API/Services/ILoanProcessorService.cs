using LoanApplicationApp.API.Models.Domain;

namespace LoanApplicationApp.API.Services
{
    public interface ILoanProcessorService
    {
        Task<LoanProcessor> FindLoanProcessorWithMinApplicationsAsync();
    }
}
