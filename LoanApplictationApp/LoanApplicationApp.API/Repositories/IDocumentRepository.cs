using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO;
using LoanApplicationApp.API.Models.DTO.Document;

namespace LoanApplicationApp.API.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document> Upload(DocumentUploadRequestDTO documentUploadRequestDTO);
    }
}
