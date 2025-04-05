using System.Text;
using System.Text.Json;
using LoanApplictationApp.Models.Domain;
using LoanApplictationApp.Models.DTO.Application;
using Microsoft.Extensions.Primitives;

namespace LoanApplictationApp.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly IConfiguration configuration;

        public ApplicationService(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<Application> AddApplicationAsync(AddApplicationDTO application, string applicantId)
        {
            string url = $"{configuration["AppSettings:LoanApplicationAPIUrl"]}/api/application?applicantId={Uri.EscapeDataString(applicantId)}" ;
            var client = httpClient.CreateClient();
            //This one works when api uses [FromBody] otherwise the values will not be passed to the api
            //var httpResponseMessage = await client.PostAsJsonAsync(url, application);

            //If api uses [FromForm] use  var httpResponseMessage = await client.PostAsync(url, content); 
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(application.MonthlySalary.ToString()),"MonthlySalary");
            content.Add(new StringContent(application.CreditScore.ToString()), "CreditScore");
            content.Add(new StringContent(application.PurchasePrice.ToString()), "PurchasePrice");
            content.Add(new StringContent(application.Deposit.ToString()), "Deposit");
            var httpResponseMessage = await client.PostAsync(url, content);
            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<Application>();
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplicationsAsync(Guid? applicantId, long? loanProcessorNo)
        {
            List<ApplicationDTO> applications = new List<ApplicationDTO>();
            StringBuilder url = new StringBuilder();
            url.Append($"{configuration["AppSettings:LoanApplicationAPIUrl"]}/api/application");
            if(applicantId != Guid.Empty  && loanProcessorNo == 0)
            {
                url.Append(applicantId);
            }
            else if(applicantId == Guid.Empty && loanProcessorNo != 0)
            {
                url.Append(loanProcessorNo);
            }

            //Fetch applications
            var client = httpClient.CreateClient();
            var httpResponseMessage = await client.GetAsync(url.ToString());
            applications.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ApplicationDTO>>());
            return applications;
        }
    }
}
