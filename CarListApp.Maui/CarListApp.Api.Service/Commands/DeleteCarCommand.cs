using MediatR;

namespace CarListApp.Api.Service.Commands;

public class DeleteCarCommand : IRequest<bool>
{
    public int Id { get; set; }
}
