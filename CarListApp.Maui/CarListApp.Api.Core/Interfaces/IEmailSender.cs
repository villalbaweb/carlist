using CarListApp.Api.Core.Dtos;

namespace CarListApp.Api.Core.Interfaces;

public interface IEmailSender
{
    Task<bool> SendEmail(SendEmailDto sendEmailDto);
}
