using LoanApplicationApp.API.Data;
using LoanApplicationApp.API.Models.Domain;

namespace LoanApplicationApp.API.Repositories
{
    public class LoanProcessorRepository : ILoanProcessorRepository
    {
        private readonly LoanApplicationDbContext loanApplicationDbContext;

        public LoanProcessorRepository(LoanApplicationDbContext loanApplicationDbContext)
        {
            this.loanApplicationDbContext = loanApplicationDbContext;
        }
        public async Task<LoanProcessor> CreatAsync(LoanProcessor loanProcessor)
        {
            loanProcessor.DateOfCreation = DateTime.Now;
            loanProcessor.IsActive = true;
            await loanApplicationDbContext.AddAsync(loanProcessor);
            await loanApplicationDbContext.SaveChangesAsync();
            return loanProcessor;
        }
    }
}
