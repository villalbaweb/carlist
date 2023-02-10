using CarListApp.Api.Core.Dtos;
using CarListApp.Api.Core.Interfaces;
using System.Net.Mail;
using System.Net;
using CarListApp.Api.Core.Settings;
using Microsoft.Extensions.Options;

namespace CarListApp.Api.Infrastructure.EmailSendProvider;

public class SmtpEmailSender : IEmailSender
{
    #region Private Members

    private readonly EmailSettings _emailSettings;

    #endregion

    #region Constructor

    public SmtpEmailSender(IOptions<EmailSettings> options)
    {
        _emailSettings = options.Value;
    }

    #endregion

    #region Public methods

    public async Task<bool> SendEmail(SendEmailDto sendEmailDto)
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
            SmtpServer.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.GmailUniquePassword);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            Console.WriteLine("Email sent successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
            
        return await Task.FromResult(true);
    }

    #endregion
}
