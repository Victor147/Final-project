using final_project.Helpers;
using MailKit.Net.Smtp;
using MimeKit;

namespace final_project.Services.EmailService;

public class EmailSender : IEmailSender
{
    public void SendEmail(MessageHelper helper)
    {
        var smtpClient = new SmtpClient();
        smtpClient.Connect("smtp.office365.com", 587, false);
        smtpClient.Authenticate("victorivanov2004@gmail.com", "0887161628Vik");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Victor", "victorivanov2004@gmail.com"));
        message.To.Add(new MailboxAddress($"{helper.Name}", $"{helper.To}"));
        message.Subject = helper.Subject;
        message.Body = new TextPart("plain") { Text = helper.Content };

        smtpClient.Send(message);
        smtpClient.Disconnect(true);
    }
}