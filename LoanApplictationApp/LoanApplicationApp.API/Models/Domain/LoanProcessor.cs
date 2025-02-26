using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoanApplicationApp.API.Models.Domain
{
    public class LoanProcessor
    {
        [Key]
        public long LoanProcessorNo { get; set; }
        public Guid ProcessorId { get; set; }
        public IEnumerable<Application> Applications { get; set; }
    }
}
