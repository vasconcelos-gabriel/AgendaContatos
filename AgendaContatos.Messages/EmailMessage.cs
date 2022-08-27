using System.Net;
using System.Net.Mail;

namespace AgendaContatos.Messages
{

    public class EmailMessage
    {
        private string _conta = "seuemail@suasenha@gmail.com";
        private string _senha = "suasenha";
        private string _smtp = "smtp-mail.outlook.com";
        private int _porta = 587;
     
    public void SendMail(string emailTo, string subject, string body)
        {
            var mailMessage = new MailMessage(_conta, emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient(_smtp, _porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.Send(mailMessage);
        }
    }
}
