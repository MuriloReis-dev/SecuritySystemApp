using System.Threading.Tasks;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;

namespace SecuritySystemApp.Views;

[QueryProperty(nameof(AlarmeId), "AlarmeId")]
public partial class AlarmePage : ContentPage
{
    public int AlarmeId { get; set; }

    private readonly AlarmeViewModel _viewModel;

    public AlarmeDetailsDTO? Dados;

    

    public AlarmePage()
    {
        InitializeComponent();

        _viewModel = new AlarmeViewModel();
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
            UsuariosList.IsVisible = false;
        else
            ListaVaziaLabel.IsVisible = false;
    }

    /// <summary>
    /// Evento ao tocar na área do alarme (apenas proprietário)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnEditAlarmeTapped(object sender, EventArgs e)
    {
        // Área para editar o nome do alarme (apenas proprietário)
        Console.WriteLine("Área do alarme tocada para editar o nome.");
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
    /// Evento ao clicar no botão de ligar/desligar o alarme
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
