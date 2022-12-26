using CarListApp.Api.Core.ValueObjects;
using MediatR;

namespace CarListApp.Api.Service.Commands;

public class CreateCarCommand : IRequest<Car>
{
    public string Make { get; set; }

    public string Model { get; set; }

    public string Vin { get; set; }

    public CreateCarCommand(string make, string model, string vin)
    {
        Make = make;
        Model = model;
        Vin = vin;
    }
}
