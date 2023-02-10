using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Service.Commands;
using MediatR;

namespace CarListApp.Api.Service.Handlers;

public class SendEmailHandler : IRequestHandler<SendEmailCommand, bool>
{
    #region Private Members

    private readonly IEmailSender _emailSender;

    #endregion


    #region Constructor

    public SendEmailHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    #endregion


    #region Public Methods

    public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        return await _emailSender.SendEmail(request.SendEmailDto);
    }

    #endregion
}
