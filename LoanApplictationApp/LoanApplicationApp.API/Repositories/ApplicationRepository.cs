using System.Text;
using LoanApplicationApp.API.Data;
using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO;
using LoanApplicationApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationApp.API.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IDbContextFactory<LoanApplicationDbContext> dbContextFactory;
        private readonly IConfiguration configuration;
        private readonly ILoanProcessorService loanProcessorService;
        private readonly IUsersService usersService;
        private readonly SmtpEmailSender smtpEmailSender;

        public ApplicationRepository(IDbContextFactory<LoanApplicationDbContext> dbContextFactory
            ,IConfiguration configuration
            ,ILoanProcessorService loanProcessorService
            ,IUsersService usersService
            ,SmtpEmailSender smtpEmailSender)
        {
            this.dbContextFactory = dbContextFactory;
            this.configuration = configuration;
            this.loanProcessorService = loanProcessorService;
            this.usersService = usersService;
            this.smtpEmailSender = smtpEmailSender;
        }
        public async Task<Application> CreatAsync(Application application, string applicantId)
        {
            var context = await dbContextFactory.CreateDbContextAsync();
            applicantId = "1edba7b4-7b63-4a15-940d-8f889bfca170";
            application.ApplicantId = "1edba7b4-7b63-4a15-940d-8f889bfca170";
            var loanProcessor = await loanProcessorService.FindLoanProcessorWithMinApplicationsAsync();
            application.LoanProcessorNo = loanProcessor.LoanProcessorNo;
            application.DateOfCreation = DateTime.Now;
            application.Status = "Under Review";
            application.RefferenceNo = "";
            await context.AddAsync(application);
            await context.SaveChangesAsync();
            application.RefferenceNo = $"{application.ApplicantId.Substring(0,8)}-{application.ApplicationNo}";
            await context.SaveChangesAsync();
            //Send Emails to Loan Procesor and Aplicant
            var applicant = await usersService.GetApplicationUserByIdAsync(applicantId);
            var body = $@"Good day {applicant.Name} {applicant.Surname}, <br><br>
                                   Thank you for submitting your loan application to 'E Home Loans'. 
                                    We have successfully received your application and are currently reviewing the details.<br>
                                    Our team will carefully assess your application and may reach out if any additional information is required. 
                                    The review process typically takes 5 business days. You will receive an update on the status of your application as soon as possible.<br>
                                    If you have any questions in the meantime, please feel free to contact us at admin@ehomeloans.co.za or 0780646700.<br>
                                    Thank you for choosing E Home Loans.
                                   <br><br>
                                   Kind Regards,<br> E Home Loans Admin.";
            var subject = "Your Loan Application Has Been Received";
            await smtpEmailSender.SendEmailAsync(applicant.UserName, subject, body);
            var processor = await usersService.GetApplicationUserByIdAsync(loanProcessor.ProcessorId);
            var body1 = $@"Good day {processor.Name} {processor.Surname}, <br><br>
                                   A new application has been assigned to you, Application No is {application.ApplicationNo}. 
                                   <br><br>
                                   Kind Regards,<br> E Home Loans Admin.";
            var subject1 = "Loan Application Assignment";
            await smtpEmailSender.SendEmailAsync(processor.UserName!, subject1, body1);
            return application;
        }

        public async Task<IEnumerable<Application>> GetAllAsync(string? ApplicantId, long? LoanProcesorNo)
        {
            var context = await dbContextFactory.CreateDbContextAsync();
            //Applications per Applicant
            if (!string.IsNullOrEmpty(ApplicantId))
            {
                return await context.Applications.Where(x => x.ApplicantId == ApplicantId).ToListAsync();
            }
            //Applicatiions per loan processor
            if (!string.IsNullOrEmpty(LoanProcesorNo.ToString()))
            {
                return await context.Applications.Where(x => x.LoanProcessorNo == LoanProcesorNo).ToListAsync();
            }
            return await context.Applications.ToListAsync();
        }

        public async Task<Application> GetByIdAsync(long id)
        {
            var context = await dbContextFactory.CreateDbContextAsync();
            return await context.Applications.FirstOrDefaultAsync(x => x.ApplicationNo == id);
        }
        public async Task<Application> UpdateAsync(long id, Application application)
        {
            var context =   await dbContextFactory.CreateDbContextAsync();
            //Check if application exist
            var existingApplication = await context.Applications.FirstOrDefaultAsync(x => x.ApplicationNo == id);
            if(existingApplication == null)
            {
                return null;
            }
            //Update existing application
            existingApplication.MonthlySalary = application.MonthlySalary;
            existingApplication.CreditScore = application.CreditScore;
            existingApplication.Status = application.Status;
            //Save new changes
            await context.SaveChangesAsync();  
            //return existing application back to the controller
            return existingApplication;
        }
    }
}
