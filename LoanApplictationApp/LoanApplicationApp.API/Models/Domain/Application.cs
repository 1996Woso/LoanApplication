using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanApplicationApp.API.Models.Domain
{
    public class Application
    {
        [Key]
        public long ApplicationNo { get; set; }
        public long LoanProcessorNo { get; set; }
        public Guid ApplicantId { get; set; }
        public double MonthlySalary { get; set; }
        public int CreditScore { get; set; }
        public string Status { get; set; }
        [NotMapped]
        public IFormFile IdCopy {  get; set; }
        public string IdCopyFilePath { get; set; }
        [NotMapped]
        public IFormFile BankStatement { get; set; }

        public string BankStatementFilePath { get; set; }
        [NotMapped]
        public IFormFile PaySlip { get; set; }

        public string PaySlipFilePath { get; set; }
        public LoanProcessor LoanProcessor { get; set; }

    }
}
