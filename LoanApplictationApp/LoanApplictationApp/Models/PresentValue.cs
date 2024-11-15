namespace LoanApplictationApp.Models
{
    public class PresentValue
    {
        public decimal? P { get; set; }//Purchase price
        public decimal? x { get; set; }//Monthly payment
        public int? n { get; set; }//Number of years
        public decimal? i { get; set; }//Interest rate
        public decimal? T { get; set; }//Total payment
        public decimal? I { get; set; }//Interest Payed
        public decimal? D { get; set; }//Deposit
    }
}
