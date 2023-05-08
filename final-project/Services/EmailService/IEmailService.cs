using final_project.Helpers;

namespace final_project.Services.EmailService;

public interface IEmailSender
{
    void SendEmail(MessageHelper helper);
}