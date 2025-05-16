using System.Data;
using System.Text;
using AutoMapper;
using LoanApplicationApp.API.Data;
using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO;
using LoanApplicationApp.API.Models.DTO.Document;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationApp.API.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        //private readonly LoanApplicationDbContext loanApplicationDbContext;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IDbContextFactory<LoanApplicationDbContext> dbContextFactory;

        public DocumentRepository(/*LoanApplicationDbContext loanApplicationDbContext,*/
            IConfiguration configuration
            ,IMapper mapper
            ,IDbContextFactory<LoanApplicationDbContext> dbContextFactory)
        {
            //this.loanApplicationDbContext = loanApplicationDbContext;
            this.configuration = configuration;
            this.mapper = mapper;
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<Document> UploadAsync(DocumentUploadRequestDTO documentUploadRequestDTO)
        {
            var saveResult = await SaveFileAsync(documentUploadRequestDTO.File);
            if (!saveResult.Success) throw new Exception(saveResult.Message);
            var documentDM = mapper.Map<Document>(documentUploadRequestDTO);
            documentDM.File = documentUploadRequestDTO.File;
            documentDM.FileExtension = Path.GetExtension(documentUploadRequestDTO.File.FileName);
            documentDM.FileSizeInBytes = documentUploadRequestDTO.File.Length;
            documentDM.FileName = saveResult.FileName;
            documentDM.DocumentType = documentUploadRequestDTO.DocumentType;
            documentDM.ApplicantId = "1edba7b4-7b63-4a15-940d-8f889bfca170";
            documentDM.FilePath = saveResult.FilePath;
            documentDM.DateOfCreation = DateTime.Now;

            var context = await  dbContextFactory.CreateDbContextAsync();
            await context.Documents.AddRangeAsync(documentDM);
            await context.SaveChangesAsync();
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
                return new FileSaveResult { Success = false, Message = "Unsupported file extension. Please upload JPG, JPEG, PDF or PNG files!" };
            }

            long maxFileSize = 2 * 1024 * 1024; // 2MB
            if (file.Length > maxFileSize)
            {
                return new FileSaveResult { Success = false, Message = "File size exceeds 2MB limit!" };
            }

            var filePath = $"{configuration["AppSettings:FilesRoothPath"]}";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            StringBuilder fileName = new StringBuilder();
            fileName.Append($"{Path.GetFileNameWithoutExtension(file.FileName)}-{DateTime.Now:yyyyMMddHHmmssff}");
            //var fileName = await GetUniqueFileNameAsync(file.FileName);

            filePath = Path.Combine(filePath, fileName.Append(fileExtension).ToString());

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new FileSaveResult { Success = true, Message = "File uploaded successfully!", FilePath = filePath ,FileName = fileName.Replace($"{fileExtension}","").ToString()};
        }

        public async Task<IEnumerable<Document>> GetAllAsync(string? userId)
        {
            var context = await dbContextFactory.CreateDbContextAsync();
            if (!string.IsNullOrEmpty(userId))
            {
                return await context.Documents.Where(x => x.ApplicantId == userId).ToListAsync();
            }
            return await context.Documents.ToListAsync();
            
        }

        public async Task<Document> DeleteAsync(Guid id)
        {
            var context = await dbContextFactory.CreateDbContextAsync();
            var document = await context.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if(document == null)
            {
                return null;
            }
            var filePath = document.FilePath;
            //Delete document from the server
            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            //Remove document record from the databas
            context.Documents.Remove(document);
            await   context.SaveChangesAsync();
            return document;
        }
        public async Task<bool> IsDeletedAsync(Guid id)
        {
            var context = await dbContextFactory.CreateDbContextAsync();
            var document = await context.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if (document == null)
            {
                return false;
            }
            var filePath = document.FilePath;
            //Delete document from the server
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            //Remove document record from the databas
            context.Documents.Remove(document);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<Document> ReplaceAsync(DocumentUploadRequestDTO documentUploadRequestDTO,Guid id)
        {
            var context = await dbContextFactory.CreateDbContextAsync();
            //Check if the document is savable (it saves it to the saver , so call SaveFileAsync and remove the file)
            var saveResult = await SaveFileAsync(documentUploadRequestDTO.File);
            if (!saveResult.Success) throw new Exception(saveResult.Message);
            if (File.Exists(saveResult.FilePath))
            {
                File.Delete(saveResult.FilePath);
            }
            //Find the existing document
            var existingDocument = await context.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if(existingDocument == null)
            {
                throw new Exception("The document you are trying to replace is not found.");
            }
            //Delete existing document
            await DeleteAsync(id);
            //Upload new document
            documentUploadRequestDTO.DocumentType = existingDocument.DocumentType;
            await UploadAsync(documentUploadRequestDTO);

            return mapper.Map<Document>(documentUploadRequestDTO);

        }
    }
}
