using System.ComponentModel.DataAnnotations.Schema;

namespace LoanApplicationApp.Models.DTO.Document
{
    public class DocumentDTO
    {
        public Guid Id { get; set; }
        public string ApplicantId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string FileName { get; set; }
        public string DocumentType { get; set; }
    }
}
