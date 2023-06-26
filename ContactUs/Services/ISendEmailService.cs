using ContactUs.Models;

namespace ContactUs.Services
{
    public interface ISendEmailService
    {
        Task<string> SendContuctUsEmail(ContactEmail contactusMessage);

    }
}
