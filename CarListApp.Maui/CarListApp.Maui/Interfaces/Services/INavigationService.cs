namespace CarListApp.Maui.Interfaces.Services;

public interface INavigationService
{
    Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);

    Task NavigateToWithAnimationAsync(string route);
}
