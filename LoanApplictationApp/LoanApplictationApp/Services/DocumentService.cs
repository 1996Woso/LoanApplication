using System.Net.Http.Headers;
using LoanApplictationApp.Models.DTO.Document;
using Microsoft.AspNetCore.Components.Forms;

namespace LoanApplictationApp.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public DocumentService(HttpClient httpClient
            ,IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<IFormFile> ConvertToIFormFile(IBrowserFile browserFile)
        {
            var stream = browserFile.OpenReadStream(maxAllowedSize: 2 * 1024 * 1024);
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

        public async Task<IEnumerable<string>> DocumentTypesAsync()
        {
            List<string> types = new List<string> { "Id","Payslip","Bank Statement" };

            return types;
        }

        // Create a method to convert IBrowserFile to IFormFile
        //private async Task<IFormFile> ConvertToIFormFile(IBrowserFile browserFile)
        //{
        //    var stream = browserFile.OpenReadStream();
        //    var memoryStream = new MemoryStream();
        //    await stream.CopyToAsync(memoryStream);
        //    memoryStream.Position = 0;

        //    return new FormFile(memoryStream, 0, memoryStream.Length,
        //                       browserFile.Name, browserFile.Name)
        //    {
        //        Headers = new HeaderDictionary(),
        //        ContentType = browserFile.ContentType
        //    };
        //}

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
                    message.Info = $"File upload failed " + await response.Content.ReadAsStringAsync();
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
