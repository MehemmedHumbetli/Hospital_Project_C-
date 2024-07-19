using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

class SendEmailWithGoogleSMTP
{
    public void Send_Email_Msg(Patient p)
    {
        string fromMail = "mehemedhumbetov1@gmail.com";
        string fromPassword = "kbdz pcgq cfon xppc";

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = "Test Subject";
        message.To.Add(new MailAddress(p.Gmail));
        message.Body = "<html><body> Registreation Completed! </body></html>";
        message.IsBodyHtml = true;

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = true,
        };

        smtpClient.Send(message);
    }
}
