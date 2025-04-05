using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanApplictationApp.Models.Domain
{
    public class Application
    {
        [Key]
        public long ApplicationNo { get; set; }
        public string RefferenceNo { get; set; }
        public long LoanProcessorNo { get; set; }
        public string ApplicantId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public double MonthlySalary { get; set; }
        public double PurchasePrice { get; set; }
        public double Deposit { get; set; }
        public int CreditScore { get; set; }
        public string Status { get; set; }
        public LoanProcessor LoanProcessor { get; set; }

    }
}
