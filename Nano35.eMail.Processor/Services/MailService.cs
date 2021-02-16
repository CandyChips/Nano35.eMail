using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using Nano35.eMail.Processor.Configuraions;

namespace Nano35.eMail.Processor.Services
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
    
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
    
    public class MailService : IMailService
    {
        private readonly MailServiceConfiguration _mailServiceConfiguration;
        public MailService(IOptions<MailServiceConfiguration> mailSettings)
        {
            _mailServiceConfiguration = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage {Sender = MailboxAddress.Parse(_mailServiceConfiguration.Mail)};
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                foreach (var file in mailRequest.Attachments.Where(file => file.Length > 0))
                {
                    byte[] fileBytes;
                    await using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailServiceConfiguration.Host, _mailServiceConfiguration.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailServiceConfiguration.Mail, _mailServiceConfiguration.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}