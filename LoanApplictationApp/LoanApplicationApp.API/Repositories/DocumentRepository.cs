using System.Text;
using AutoMapper;
using LoanApplicationApp.API.Data;
using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO;
using LoanApplicationApp.API.Models.DTO.Document;

namespace LoanApplicationApp.API.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly LoanApplicationDbContext loanApplicationDbContext;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public DocumentRepository(LoanApplicationDbContext loanApplicationDbContext
            ,IConfiguration configuration
            ,IMapper mapper)
        {
            this.loanApplicationDbContext = loanApplicationDbContext;
            this.configuration = configuration;
            this.mapper = mapper;
        }
        public async Task<Document> Upload(DocumentUploadRequestDTO documentUploadRequestDTO)
        {
            var saveResult = await SaveFileAsync(documentUploadRequestDTO.File);
            if (!saveResult.Success) throw new Exception(saveResult.Message);
            var documentDM = mapper.Map<Document>(documentUploadRequestDTO);
            documentDM.File = documentUploadRequestDTO.File;
            documentDM.FileExtension = Path.GetExtension(documentUploadRequestDTO.File.FileName);
            documentDM.FileSizeInBytes = documentUploadRequestDTO.File.Length;
            documentDM.FileName = documentUploadRequestDTO.File.FileName;
            documentDM.DocumentType = documentUploadRequestDTO.DocumentType;
            documentDM.ApplicantId = "1edba7b4-7b63-4a15-940d-8f889bfca170";
            documentDM.FilePath = saveResult.FilePath;
            documentDM.DateOfCreation = DateTime.Now;

            await loanApplicationDbContext.Documents.AddRangeAsync(documentDM);
            await loanApplicationDbContext.SaveChangesAsync();
            return documentDM;
        }
        public async Task<string> GetUniqueFileNameAsync(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName);
            fileName = Path.GetFileNameWithoutExtension(fileName);
            string filePath = $"{configuration["AppSettings:FilesRoothPath"]}";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            StringBuilder uniqueName = new StringBuilder(fileName);
            int count = 1;
            while (System.IO.File.Exists(Path.Combine(filePath, uniqueName.ToString() + fileExtension)))
            {
                uniqueName.Append($"{count++}");
            }
            return uniqueName.Append(fileExtension).ToString();
        }
        public async Task<FileSaveResult> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new FileSaveResult { Success = false, Message = "File is empty or missing." };
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var allowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png", ".pdf" };

            if (!allowedExtensions.Contains(fileExtension))
            {
                return new FileSaveResult { Success = false, Message = "Unsupported file extension. Please upload JPG, JPEG, PDF or PNG files." };
            }

            long maxFileSize = 2 * 1024 * 1024; // 2MB
            if (file.Length > maxFileSize)
            {
                return new FileSaveResult { Success = false, Message = "File size exceeds 2MB limit." };
            }

            var filePath = $"{configuration["AppSettings:FilesRoothPath"]}";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fileName = await GetUniqueFileNameAsync(file.FileName);
            filePath = Path.Combine(filePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new FileSaveResult { Success = true, FilePath = filePath };
        }
    }
}
