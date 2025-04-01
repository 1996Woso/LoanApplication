using LoanApplicationApp.API.Models.Domain;

namespace LoanApplicationApp.API.Repositories
{
    public interface ILoanProcessorRepository
    {
        Task<LoanProcessor> CreatAsync(LoanProcessor loanProcessor);
        //Task<LoanProcessor> UpdateAsync(LoanProcessor loanProcessor);
        //Task<LoanProcessor> GetAllAsync(LoanProcessor loanProcessor);
        //Task<LoanProcessor> GetByIdAsync(int id);
    }
}
