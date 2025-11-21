using System.Threading.Tasks;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Services;

namespace SecuritySystemApp.Views;

[QueryProperty(nameof(AlarmeId), "AlarmeId")]
public partial class AlarmePage : ContentPage
{
    public int AlarmeId { get; set; }

    private readonly AlarmeViewModel _viewModel;
    private readonly NavigationService _navigationService;

    public AlarmeDetailsDTO? Dados;

    

    public AlarmePage()
    {
        InitializeComponent();

        _viewModel = new AlarmeViewModel();
        _navigationService = new NavigationService();
    }

    /// <summary>
    /// Carrega os dados do alarme ao aparecer a página
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Dados = await _viewModel.CarregarAlarmeAsync(AlarmeId);

        UsuariosList.ItemsSource = Dados?.Usuarios;

        if (Dados != null)
            BindingContext = Dados;

        // Ajusta visibilidade dos elementos com base nos dados carregados
        if (Dados == null || Dados.Usuarios?.Count == 0)
            UsuariosGrid.IsVisible = false;
        else
            ListaVaziaLabel.IsVisible = false;
    }

    /// <summary>
    /// Evento ao tocar na área do alarme (apenas proprietário)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public async void OnEditAlarmeTapped(object sender, EventArgs e)
    {
        if (Dados == null || Dados.Alarme == null || Dados.Alarme.TipoPermissao != true)
        {
            Console.WriteLine("Usuário não é proprietário, não permite editar o alarme.");
            return; // Usuário não é proprietário, não permite editar
        }

        // Área para editar o nome do alarme (apenas proprietário)
        string novoNome = await DisplayPromptAsync("Editar Alarme", "Digite o novo nome do alarme:", initialValue: Dados?.Alarme?.Nome);
        if (novoNome == null)
            return; // Usuário cancelou a ação
        if (!string.IsNullOrEmpty(novoNome) && Dados != null && Dados.Alarme != null)
        {
            bool sucesso = await _viewModel.EditarAlarmeAsync(Dados.Alarme.Id, novoNome);
            if (sucesso)
            {
                await DisplayAlert("Sucesso", "Nome do alarme atualizado com sucesso.", "OK");
                OnAppearing(); // Recarrega a página para refletir a mudança
            }
            else
            {
                await DisplayAlert("Erro", "Não foi possível atualizar o nome do alarme.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Erro", "Dados do alarme não carregados ou nome inválido.", "OK");
        }
    }

    /// <summary>
    /// Evento ao alternar o estado do alarme
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public async void OnToggleAlarmClicked(object sender, EventArgs e)
    {
        if (Dados != null && Dados.Alarme != null)
        {
            if (Dados.Alarme.TipoPermissao != true)
            {
                Console.WriteLine("Usuário sem permissão para alterar o estado do alarme.");
                return; // Usuário sem permissão
            }

            bool sucesso = await _viewModel.AlarmeOnOffAsync(Dados.Alarme.Id, !Dados.Alarme.Ligado);
            if (!sucesso)
            {
                await DisplayAlert("Erro", "Não foi possível alterar o estado do alarme.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Erro", "Dados do alarme não carregados.", "OK");
        }

        OnAppearing(); // Recarrega a página
    }

    /// <summary>
    /// Evento ao clicar no botão de liberar acesso
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public async void OnLiberarClicked(object sender, EventArgs e)
    {
        if (Dados != null && Dados.Alarme != null)
        {
            bool sucesso = await _viewModel.LiberarAcessoAsync(Dados.Alarme.Id);
            if (sucesso)
            {
                await IniciarTimer();
            }
            else
                await DisplayAlert("Erro", "Não foi possível liberar o acesso ao alarme.", "OK");
        }
        else
            await DisplayAlert("Erro", "Dados do alarme não carregados.", "OK");
    }

    public async void OnAddUsuarioClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(CadastroPessoaPage));
    }

    public async Task IniciarTimer()
    {
        Shell.SetNavBarIsVisible(this, false);
        Overlay.IsVisible = true;
        int segundos = 10;

        for (int i = segundos; i > 0; i--)
        {
            TempoLabel.Text = $"Alarme será ativado em {i} segundos";
            await Task.Delay(1000);
        }
        Shell.SetNavBarIsVisible(this, true);
        Overlay.IsVisible = false;
    }
}
