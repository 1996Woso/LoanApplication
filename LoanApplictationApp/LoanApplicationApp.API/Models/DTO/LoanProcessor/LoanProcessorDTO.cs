using System.ComponentModel.DataAnnotations;

namespace LoanApplicationApp.API.Models.DTO.LoanProcessor
{
    public class LoanProcessorDTO
    {
        [Key]
        public long LoanProcessorNo { get; set; }
        public string ProcessorId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool IsActive { get; set; }
    }
}
