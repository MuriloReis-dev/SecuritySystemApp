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
    public void OnAlarmAreaTapped(object sender, EventArgs e)
    {
        // Área para editar o nome do alarme (apenas proprietário)
        Console.WriteLine("Área do alarme tocada para editar o nome.");
    }

    /// <summary>
    /// Evento ao clicar no botão de ligar/desligar o alarme
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public async void OnLiberarClicked(object sender, EventArgs e)
    {
        if (Dados != null && Dados.Alarme != null)
            await _viewModel.LiberarAcessoAsync(Dados.Alarme.Id);
        else
            Console.WriteLine("Id do Alarme não pode ser nulo.");

        OnAppearing(); // Recarrega os dados
    }
}
