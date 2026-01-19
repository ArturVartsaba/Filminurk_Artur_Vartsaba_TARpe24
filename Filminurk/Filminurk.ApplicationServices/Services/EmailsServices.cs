using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Environment = Filminurk.Data.Environment;
namespace Filminurk.ApplicationServices.Services
{
    public class EmailsServices : IEmailsServices
    {
        private readonly IConfiguration _configuration;

        public EmailsServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(EmailDTO dto)
        {
            var email = new MimeMessage();
            _configuration.GetSection("EmailUserName").Value = Environment.gmailusername;
            _configuration.GetSection("EmailHost").Value = Environment.smtpaddress;
            _configuration.GetSection("EmailPassword").Value = Environment.gmailapppassword;

            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(dto.SendToThisAddress));
            email.Subject = dto.EmailSubject;
            var builder = new BodyBuilder
            {
                HtmlBody = dto.EmailContent,
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        
        public void SendRegistrationEmail(EmailDTO dto, EmailTokenDTO token)
        {
            var email = new MimeMessage();
            _configuration.GetSection("EmailUserName").Value = Environment.gmailusername;
            _configuration.GetSection("EmailHost").Value = Environment.smtpaddress;
            _configuration.GetSection("EmailPassword").Value = Environment.gmailapppassword;

            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(dto.SendToThisAddress));
            email.Subject = dto.EmailSubject;
            var builder = new BodyBuilder
            {
                HtmlBody = dto.EmailContent,
                //HtmlBody = $"{"Sinu konto on edukalt loodud"}<br/><br/>Token: {token.Token}",
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
