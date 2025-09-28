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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Dados = await _viewModel.CarregarAlarmeAsync(AlarmeId);

        UsuariosList.ItemsSource = Dados?.Usuarios;

        if (Dados != null)
            BindingContext = Dados;
    }

    public void OnAlarmAreaTapped(object sender, EventArgs e)
    {
        // Área para editar o nome do alarme (apenas proprietário)
        Console.WriteLine("Área do alarme tocada para editar o nome.");
    }

    public async void OnToggleAlarmClicked(object sender, EventArgs e)
    {
        if (Dados != null && Dados.Alarme != null)
            await _viewModel.AlarmeOnOffAsync(Dados.Alarme.Id, !Dados.Alarme.Ligado);
        else
            Console.WriteLine("Id do Alarme não pode ser nulo.");
        
        OnAppearing(); // Recarrega os dados
    }
}
