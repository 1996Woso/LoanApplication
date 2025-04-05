using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using LoanApplicationApp.Models.Domain;
using LoanApplicationApp.Models.DTO.Document;
using LoanApplictationApp.Models.DTO.Document;
using Microsoft.AspNetCore.Components.Forms;

namespace LoanApplictationApp.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        public DocumentService(HttpClient httpClient
            ,IConfiguration configuration
            ,IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }
        // Create a method to convert IBrowserFile to IFormFile
        public async Task<IFormFile> ConvertToIFormFile(IBrowserFile browserFile)
        {
            long maxFileSize = 1000 * 1024 * 1024; ;
            if(browserFile.Size > maxFileSize )
            {
                throw new IOException($"File size exceeds the maximum allowed limit of {maxFileSize / (1024 * 1024)}MB.");
            }
            var stream = browserFile.OpenReadStream(maxFileSize);
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return new FormFile(memoryStream, 0, memoryStream.Length,
                               browserFile.Name, browserFile.Name)
            {
                Headers = new HeaderDictionary(),
                ContentType = browserFile.ContentType
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {

                string url = $"{configuration["AppSettings:LoanApplicationAPIUrl"]}/api/document/{id}";
                var response = await httpClient.DeleteAsync(url) ;
                if (response.IsSuccessStatusCode)
                {
                    return true ;
                }
                else
                {
                    return false ;
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<string>> DocumentTypesAsync()
        {
            List<string> types = new List<string> { "Id","Payslip","Bank Statement" };

            return types;
        }
        public async Task<IEnumerable<DocumentDTO>> GetUserDocumentsAsync(string? userId)
        {
            try
            {
                string url = $"{configuration["AppSettings:LoanApplicationAPIUrl"]}/api/document?userId={userId}";
                var response = await httpClient.GetFromJsonAsync<IEnumerable<DocumentDTO>>(url);
                return response ?? Enumerable.Empty<DocumentDTO>();
            }
            catch (HttpRequestException ex)
            {
                // Handle or log the error appropriately
                Console.WriteLine($"Error fetching documents: {ex.Message}");
                return Enumerable.Empty<DocumentDTO>();
            }
        }

        public async Task<FileMessage> UploadAsync(IBrowserFile file, string documentType)
        {
            FileMessage message = new FileMessage();
            string url = $"{configuration["AppSettings:LoanApplicationAPIUrl"]}/api/document/Upload";
            try
            {
                var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(file.OpenReadStream(2 * 1024 * 1024));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                content.Add(fileContent, "File", file.Name);
                content.Add(new StringContent(documentType), "DocumentType");

                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    message.Success = "File uploaded successfully!";
                    return message;
                }
                else
                {
                    message.Info = $"File upload failed: " + await response.Content.ReadAsStringAsync();
                    return message;
                }
            }
            catch (Exception ex)
            {
                message.Error = $"Error: {ex.Message}";
                return message;
            }
        }
    }
}
