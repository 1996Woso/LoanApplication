using LoanApplicationApp.API.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanApplicationApp.API.Models.DTO.Application
{
    public class AddApplicationDTO
    {
        public double MonthlySalary { get; set; }
        public double PurchasePrice { get; set; }
        public double Deposit { get; set; }
        public int CreditScore { get; set; }

    }
}
