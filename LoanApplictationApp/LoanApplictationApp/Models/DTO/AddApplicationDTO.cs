using Microsoft.AspNetCore.Components.Forms;

namespace LoanApplictationApp.Models.DTO
{
    public class AddApplicationDTO
    {
        public double MonthlySalary { get; set; }
        public double PurchasePrice { get; set; }
        public double Deposit { get; set; }
        public int CreditScore { get; set; }
    }

}
