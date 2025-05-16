using LoanApplicationApp.API.Data;
using LoanApplicationApp.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

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
            //Check if loanProcessor exists(if it exists update IsActive to true
            var existingLoanProcessor = await loanApplicationDbContext.LoanProcessors.FirstOrDefaultAsync(x => x.ProcessorId == loanProcessor.ProcessorId);
            if(existingLoanProcessor != null)
            {
                loanProcessor.IsActive = true;
                await UpdateAsync(loanProcessor.ProcessorId, loanProcessor);
                return loanProcessor;
            }
            loanProcessor.DateOfCreation = DateTime.Now;
            loanProcessor.IsActive = true;
            await loanApplicationDbContext.AddAsync(loanProcessor);
            await loanApplicationDbContext.SaveChangesAsync();
            return loanProcessor;
        }

        public async Task<LoanProcessor> UpdateAsync(string userId, LoanProcessor loanProcessor)
        {
           var existingLoanProcessor = await loanApplicationDbContext.LoanProcessors.FirstOrDefaultAsync(x => x.ProcessorId == userId);
            if(existingLoanProcessor == null)
            {
                return null;
            }
            //Update Existing loan processor
            existingLoanProcessor.IsActive = loanProcessor.IsActive;
            await loanApplicationDbContext.SaveChangesAsync();
            return existingLoanProcessor;
        }
    }
}
