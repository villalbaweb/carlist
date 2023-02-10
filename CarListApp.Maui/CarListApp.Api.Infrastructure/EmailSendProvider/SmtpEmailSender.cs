using CarListApp.Api.Core.Dtos;
using CarListApp.Api.Core.Interfaces;
using System.Net.Mail;
using System.Net;

namespace CarListApp.Api.Infrastructure.EmailSendProvider;

public class SmtpEmailSender : IEmailSender
{
    #region Constructor

    public SmtpEmailSender()
    {

    }

    #endregion

    #region Public methods

    public void SendEmail(SendEmailDto sendEmailDto)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(sendEmailDto.From);
            mail.To.Add(sendEmailDto.To);
            mail.Subject = sendEmailDto.Subject;
            mail.Body = sendEmailDto.Body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("villalbaweb@gmail.com", "oervzqtacvdeixop");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            Console.WriteLine("Email sent successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    #endregion
}
