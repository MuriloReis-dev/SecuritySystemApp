namespace SecuritySystemApp.Interfaces;

public interface INavigationService
{
    Task NavegarParaAsync(string pageName);
    Task VoltarAsync();
}