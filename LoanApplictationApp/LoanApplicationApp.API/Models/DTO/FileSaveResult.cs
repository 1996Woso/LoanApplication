namespace LoanApplicationApp.API.Models.DTO
{
    public class FileSaveResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
