using CarListApp.Maui.Interfaces.Services;

namespace CarListApp.Maui.Services;

public class DisplayAlertService : IDisplayAlertService
{
    #region Public Methods

    public Task DisplayAlertAsync(string title, string message, string cancel)
    {
        return Shell.Current.DisplayAlert(title, message, cancel);
    }

    #endregion
}
