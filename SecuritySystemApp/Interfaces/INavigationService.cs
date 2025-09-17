namespace SecuritySystemApp.Interfaces;

public interface INavigationService
{
    Task NavegarAsync(string pageName);
    Task NavegarAsync(string pageName, Dictionary<string, object> parameters);
    Task NavegarResetAsync(string pageName);
    Task VoltarAsync();
}