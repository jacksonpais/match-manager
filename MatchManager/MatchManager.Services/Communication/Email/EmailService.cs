using MatchManager.Services.Communication.Email.Interface;
using MatchManager.Services.Communication.Email.Model;
using System.Net.Mail;

namespace MatchManager.Services.Communication.Email
{
    public class EmailService : IEmailService
    {
        public required string Subject { get; set; }
        public required string Message { get; set; }
        public required EmailReceiver Receiver { get; set; }
        public required EmailProperties EmailProperties { get; set; }

        public async Task<bool> SendAsync()
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                MailAddress mailAddress = new MailAddress(EmailProperties.Email, EmailProperties.DisplayName);
                message.From = mailAddress;
                message.To.Add(Receiver.ToAddress);
                message.Subject = Subject;
                message.IsBodyHtml = true;

                if (Receiver.Bcc != null)
                {
                    foreach (var address in Receiver.Bcc.Where(bccValue => !string.IsNullOrWhiteSpace(bccValue)))
                    {
                        message.Bcc.Add(address.Trim());
                    }
                }

                if (Receiver.Cc != null)
                {
                    foreach (var address in Receiver.Cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue)))
                    {
                        message.CC.Add(address.Trim());
                    }
                }

                message.Body = Message;

                smtpClient.Host = "relay-hosting.secureserver.net";
                smtpClient.Port = 25;

                //smtpClient.Host = messageTemplate.EmailProperties.Host;
                //smtpClient.Port = messageTemplate.EmailProperties.Port;

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(EmailProperties.Email, EmailProperties.Password);

                //smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(message);
                smtpClient.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
