using CarListApp.Api.Core.Dtos;

namespace CarListApp.Api.Core.Interfaces;

public interface IEmailSender
{
    void SendEmail(SendEmailDto sendEmailDto);
}
