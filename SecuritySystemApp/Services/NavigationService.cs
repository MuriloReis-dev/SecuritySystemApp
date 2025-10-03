using SecuritySystemApp.Interfaces;

namespace SecuritySystemApp.Services;

public class NavigationService : INavigationService
{
    /// <summary>
    /// Navega para a página especificada
    /// </summary>
    /// <param name="pageName">Nome da página destino</param>
    public async Task NavegarAsync(string pageName)
    {
        await Shell.Current.GoToAsync(pageName, animate: false);
    }

    /// <summary>
    /// Navega para a página especificada com parâmetros
    /// </summary>
    /// <param name="pageName">Nome da página destino</param>
    /// <param name="parameters">Dicionário de parâmetros enviados na navegação</param>
    public async Task NavegarAsync(string pageName, Dictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync(new ShellNavigationState(pageName), animate: false, parameters);
    }

    /// <summary>
    /// Navega para a página especificada resetando a pilha de navegação
    /// </summary>
    /// <param name="pageName">Nome da página destino</param>
    public async Task NavegarResetAsync(string pageName)
    {
        await Shell.Current.GoToAsync($"//{pageName}", animate: false);
    }

    /// <summary>
    /// Navega para a página anterior
    /// </summary>
    public async Task VoltarAsync()
    {
        await Shell.Current.GoToAsync("..", animate: false);
    }
}