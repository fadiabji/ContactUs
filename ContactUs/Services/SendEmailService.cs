using ContactUs.Models;

namespace ContactUs.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SendEmailService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("sendemail");
            _configuration = configuration;
        }

   


        public async Task<string> SendContuctUsEmail(ContactEmail contactusMessage)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(_configuration["AzureKeyRequestAddress"], contactusMessage);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return "Sorry! Some error ocurred";
            }
            return "Thank you for contacting us, your message submitted Successfully";
        }
    }
}
