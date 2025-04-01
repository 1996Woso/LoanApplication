using LoanApplicationApp.API.Data;
using LoanApplicationApp.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationApp.API.Services
{
    public class LoanProcessorService : ILoanProcessorService
    {
        private readonly LoanApplicationDbContext applicationDbContext;

        public LoanProcessorService(LoanApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<LoanProcessor> FindLoanProcessorWithMinApplicationsAsync()
        {
            var loanProcessors = await applicationDbContext.LoanProcessors.Where(x => x.IsActive == true).ToListAsync();

            // Calculate application count directly in the database
            var applicationCounts = await applicationDbContext.Applications
                .GroupBy(a => a.LoanProcessorNo)
                .Select(g => new { LoanProcessorNo = g.Key, ApplicationCount = g.Count() })
                .ToListAsync();
            // Merge loan processors with their application count (including those with zero applications)
            var processorAppCounts = loanProcessors
                .Select(lp => new
                {
                    LoanProcessor = lp,
                    ApplicationCount = applicationCounts.FirstOrDefault(ac => ac.LoanProcessorNo == lp.LoanProcessorNo)?.ApplicationCount ?? 0
                })
                .ToList();
            // Find the minimum application count
            int minApplications = processorAppCounts.Min(x => x.ApplicationCount);

            // Get all loan processors with the minimum application count
            var processorsWithMinApplications = processorAppCounts
                .Where(x => x.ApplicationCount == minApplications)
                .Select(x => x.LoanProcessor)
                .ToList();

            // Select one randomly
            Random random = new Random();
            return processorsWithMinApplications[random.Next(processorsWithMinApplications.Count)];
        }
    }
}
