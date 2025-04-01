namespace LoanApplicationApp.API.Models.DTO.Application
{
    public class UpdateApplicationRequestDTO
    {
        public long LoanProcessorNo { get; set; }
        public double MonthlySalary { get; set; }
        public int CreditScore { get; set; }
        public string Status { get; set; }
    }
}
