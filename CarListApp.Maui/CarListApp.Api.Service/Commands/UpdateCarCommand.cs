using MediatR;

namespace CarListApp.Api.Service.Commands;

public class UpdateCarCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public string Vin { get; set; }

    public UpdateCarCommand(int id, string make, string model, string vin)
    {
        Id = id;
        Make = make;
        Model = model;
        Vin = vin;
    }
}
