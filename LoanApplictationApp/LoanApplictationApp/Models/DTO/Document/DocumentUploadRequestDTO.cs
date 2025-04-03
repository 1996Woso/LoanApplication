using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoanApplicationApp.Models.DTO.Document
{
    public class DocumentUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required(ErrorMessage ="The Document Type Field is required")]
        public string DocumentType { get; set; }
    }
}
