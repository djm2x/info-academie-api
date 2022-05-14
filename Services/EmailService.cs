using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Providers;
using System;

namespace Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly AppSettings AppSettings;
        private IWebHostEnvironment env;
        private readonly TokkenHandler _tokkenHandler;

        private string domain = "";
        public EmailService(IOptions<EmailSettings> emailSettings
        , IOptions<AppSettings> AppSettings
        , IWebHostEnvironment hostingEnvironment
        , TokkenHandler tokkenHandler
        )
        {
            _emailSettings = emailSettings.Value;
            this.AppSettings = AppSettings.Value;
            this.env = hostingEnvironment;
             _tokkenHandler = tokkenHandler;

            domain = env.IsDevelopment() ? "http://localhost:4200" : this.AppSettings.DomainUrlForEmail;
        }

        public async Task newAccount(EmailOption option)
        {
            var html = await System.IO.File.ReadAllTextAsync($"db/welcome.fr.html");

            var link = (env.IsDevelopment() ? "http://localhost:4200" : AppSettings.DomainUrlForEmail) + "/sign-in";
            html = html
                .Replace("{{compte_route}}", link)
                .Replace("{{customerName}}", option.CustomerName)
                ;

            await SendEmailAsync(option.CustomerEmail, option.CustomerName, "[Info-Academie] Welcome!", html);
        }

        public async Task ValidateAccount(EmailOption option)
        {
            var html = await System.IO.File.ReadAllTextAsync($"db/validate-account.fr.html");

            option.UrlToken = $"{domain}/reset-password/{GenerateToken(option.CustomerEmail)}";

            html = html.Replace("{{url_token}}", option.UrlToken)
                .Replace("{{Adresse postale}}", "12030")
                ;

            await SendEmailAsync(option.CustomerEmail, option.CustomerName, "[Info-Academie] Validate Account", html);
        }

        public async Task ForgotPassword(EmailOption option)
        {
            var html = await System.IO.File.ReadAllTextAsync($"db/reset-pwd.fr.html");

            option.UrlToken = $"{domain}/reset-password/{GenerateToken(option.CustomerEmail)}";

            html = html.Replace("{{url_token}}", option.UrlToken)
                .Replace("{{Adresse postale}}", "12030")
                ;

            await SendEmailAsync(option.CustomerEmail, option.CustomerName, "[Info-Academie] Reset Password", html);
        }


        public async Task passwordChanged(EmailOption option)
        {
            var html = await System.IO.File.ReadAllTextAsync($"db/pwd-changed.html");

            await SendEmailAsync(option.CustomerEmail, option.CustomerName, "[Info-Academie] Password Changed", html);
        }

        private string GenerateToken(string email)
        {
            var claims = new Claim[] { new Claim("email",email), new Claim("creation", DateTime.Now.ToString()) };

            return _tokkenHandler.GenerateTokken(claims);
        }

        private async Task SendEmailAsync(string email, string name, string subject, string msg)
        {
            
                var smtpClient = new SmtpClient
                {
                    Host = _emailSettings.MailServer, // set your SMTP server name here
                    Port = _emailSettings.MailPort.Value, // Port 
                    Credentials = new NetworkCredential(_emailSettings.MailAccount, _emailSettings.Password),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                };
                
                // using (var message = new MailMessage(_emailSettings.SenderEmail, email)
                using (var message = new MailMessage(
                    new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    new MailAddress(email, name)
                )
                {
                    Subject = subject,
                    Body = msg,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal,
                })
                {
                    await smtpClient.SendMailAsync(message);
                }
        }

        // private Task SendEmailsAsync(string emails, string name, string subject, string message)
        // {
        //     var smtpClient = new SmtpClient
        //     {
        //         Host = _emailSettings.MailServer, // set your SMTP server name here
        //         Port = _emailSettings.MailPort, // Port 
        //         EnableSsl = true,
        //         UseDefaultCredentials = false,
        //         Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password),
        //         Timeout = 30000
        //     };

        //     using (var Message = new MailMessage()
        //     {
        //         From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
        //         Subject = subject,
        //         Body = message,
        //         IsBodyHtml = true,
        //         Priority = MailPriority.Normal,
        //     })
        //     {
        //         // message.CC.Add("mohamed.mourabit@outlook.com");
        //         foreach (var email in emails.Split(";"))
        //         {
        //             if (email != "")
        //             {
        //                 Message.To.Add(new MailAddress(email, name));
        //             }
        //         }

        //         return smtpClient.SendMailAsync(Message);
        //     }
        // }
    }
}