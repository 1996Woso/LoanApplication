using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace LoanApplictationApp.Models.DTO.Application
{
    public class AddApplicationDTO
    {
        [Required]
        [Range(1500, 10000000, ErrorMessage = "Monthly Salary must range between R1,500.00 and R10,000,000.00 .")]
        public double MonthlySalary { get; set; }
        [Required]
        [Range(100000, 50000000, ErrorMessage = "Purchase Price must range between R100,000.00 and R50,000,000.00 .")]
        public double PurchasePrice { get; set; }
        [Required]
        [Range(0, 25000000, ErrorMessage = "Deposit must range between R0.00 and R25,000,000.00 .")]
        public double Deposit { get; set; }
        [Required]
        [Range(100, 1200, ErrorMessage = "Credit Score must range between 100 and 1,200 .")]
        public int CreditScore { get; set; }
    }

}
