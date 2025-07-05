using SecuritySystemApp.Interfaces;

namespace SecuritySystemApp.Services;

public class NavigationService : INavigationService
{
    public async Task NavegarParaAsync(string pageName)
    {
        await Shell.Current.GoToAsync(pageName);
    }

    public async Task VoltarAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}