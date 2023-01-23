using CarListApp.Maui.Interfaces.Services;

namespace CarListApp.Maui.Services;

public class NavigationService : INavigationService
{
    #region Public Methods

    public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
    {
        var shellNavigation = new ShellNavigationState(route);

        return routeParameters != null
            ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
            : Shell.Current.GoToAsync(shellNavigation);
    }

    public Task NavigateToWithAnimationAsync(string route)
    {
        var shellNavigation = new ShellNavigationState(route);

        return Shell.Current.GoToAsync(shellNavigation, true);
    }

    #endregion
}
