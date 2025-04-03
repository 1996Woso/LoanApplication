using LoanApplicationApp.Models.DTO.Document;
using LoanApplictationApp.Models.DTO.Document;
using Microsoft.AspNetCore.Components.Forms;

namespace LoanApplictationApp.Services
{
    public interface IDocumentService
    {
        Task<FileMessage> UploadAsync(IBrowserFile file, string documentType);
        Task<IEnumerable<string>> DocumentTypesAsync();
        Task<IFormFile> ConvertToIFormFile(IBrowserFile browserFile);
    }
}
