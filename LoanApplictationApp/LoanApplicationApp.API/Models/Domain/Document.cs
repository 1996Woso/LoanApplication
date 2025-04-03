using System.ComponentModel.DataAnnotations.Schema;

namespace LoanApplicationApp.API.Models.Domain
{
    public class Document
    {
        public Guid Id { get; set; }
 
        public DateTime DateOfCreation { get; set; }
        public string ApplicantId { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
