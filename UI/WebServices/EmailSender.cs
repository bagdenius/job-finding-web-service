using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace UI.WebServices
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor) =>
            Options = optionsAccessor.Value;
        public AuthMessageSenderOptions Options { get; }

        public Task SendEmailAsync(string email, string subject, string message) =>
            Execute(Options.SendGridKey, subject, message, email);

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            apiKey = "";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);
            return client.SendEmailAsync(msg);
        }
    }
}
