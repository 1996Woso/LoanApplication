namespace LoanApplictationApp.Models
{
    public class Affordability:PresentValue
    {
        public decimal? GrossIncome { get; set; } //Monthly gross salary
        public  decimal? MaxAffordInstall { get; set; }
        public decimal? MaxAffordLoan { get; set; }
    }
}
