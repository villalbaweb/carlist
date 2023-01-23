namespace CarListApp.Maui.Interfaces.Services;

public interface IDisplayAlertService
{
    Task DisplayAlertAsync(string title, string message, string cancel);
}
