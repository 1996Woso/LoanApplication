using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO;
using LoanApplicationApp.API.Models.DTO.Document;

namespace LoanApplicationApp.API.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document> UploadAsync(DocumentUploadRequestDTO documentUploadRequestDTO);
        Task<IEnumerable<Document>> GetAllAsync(string? userId);
        Task<Document> DeleteAsync(Guid id);
        Task<Document> ReplaceAsync(DocumentUploadRequestDTO documentUploadRequestDTO,Guid id);
        Task<bool> IsDeletedAsync(Guid id);
    }
}
