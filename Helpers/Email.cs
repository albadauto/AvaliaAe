using AvaliaAe.Helpers.Interfaces;
using System.Net;
using System.Net.Mail;

namespace AvaliaAe.Helpers
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        public bool SendMail(string email, string subject, string message)
        {
            try
            {
                var all = new 
                {
                    host = _configuration.GetValue<string>("SMTP:Host"),
                    name = _configuration.GetValue<string>("SMTP:Name"), 
                    username = _configuration.GetValue<string>("SMTP:UserName"), 
                    password = _configuration.GetValue<string>("SMTP:Password"),
                    port = _configuration.GetValue<int>("SMTP:Port"),
                };
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(all.username, all.name),
                };

                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(all.host, all.port))
                {
                    smtp.Credentials = new NetworkCredential(all.username, all.password);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
