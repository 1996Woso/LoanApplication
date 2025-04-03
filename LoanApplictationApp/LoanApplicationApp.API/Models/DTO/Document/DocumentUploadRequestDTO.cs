using System.ComponentModel.DataAnnotations;

namespace LoanApplicationApp.API.Models.DTO.Document
{
    public class DocumentUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string DocumentType { get; set; }
    }
}
