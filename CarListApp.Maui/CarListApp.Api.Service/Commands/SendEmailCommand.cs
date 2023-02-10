using CarListApp.Api.Core.Dtos;
using MediatR;

namespace CarListApp.Api.Service.Commands;

public class SendEmailCommand : IRequest<bool>
{
    public SendEmailDto SendEmailDto { get; set; }

	public SendEmailCommand(SendEmailDto sendEmailDto)
	{
		SendEmailDto= sendEmailDto;
	}
}
