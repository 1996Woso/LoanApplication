using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoanApplicationApp.API.Models.Domain
{
    public class LoanProcessor
    {
        [Key]
        public long LoanProcessorNo { get; set; }
        public string ProcessorId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Application> Applications { get; set; }
    }
}
