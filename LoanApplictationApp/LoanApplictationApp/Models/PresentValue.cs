using System.ComponentModel.DataAnnotations;

namespace LoanApplictationApp.Models
{
    public class PresentValue
    {
        [Range(0,5000000,ErrorMessage ="Loan amount must be between 0 and 5000000")]
        public decimal? P { get; set; }//Purchase price
        [Range(0, 50000, ErrorMessage = "Monthly payment must be between 0 and 50000")]
        public decimal? x { get; set; }//Monthly payment
        [Range(0,30,ErrorMessage ="Period must be between 0 and 30")]
        public int? n { get; set; } = 20;//Number of years
        [Range(0,11.5,ErrorMessage ="Interest rate must be between 0 and 11.5")]
        public decimal? i { get; set; } = 11.50m;//Interest rate
        public decimal? T { get; set; }//Total payment
        public decimal? I { get; set; }//Interest Payed
        [Range(0, 3000000, ErrorMessage = "Deposit amount must be between 0 and 3000000")]
        public decimal? D { get; set; } = 0.00m;//Deposit
    }
}
