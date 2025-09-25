using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;

namespace SecuritySystemApp.Views;

[QueryProperty(nameof(AlarmeId), "AlarmeId")]
public partial class AlarmePage : ContentPage
{
    private int AlarmeId { get; set; }

    private readonly AlarmeViewModel _viewModel;

    public AlarmeDTO? Alarme;

    public AlarmePage()
    {
        InitializeComponent();

        _viewModel = new AlarmeViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Alarme = await _viewModel.CarregarAlarmeAsync(AlarmeId);

        if (Alarme != null)
            BindingContext = Alarme;
    }

    public void OnAlarmAreaTapped(object sender, EventArgs e)
    {
        // Área para editar o nome do alarme (apenas proprietário)
        Console.WriteLine("Área do alarme tocada para editar o nome.");
    }

    public async void OnToggleAlarmClicked(object sender, EventArgs e)
    {
        if (Alarme != null && Alarme != null)
            await _viewModel.AlarmeOnOffAsync(Alarme.Id_Alarme, !Alarme.Ligado);
        else
            Console.WriteLine("Id do Alarme não pode ser nulo.");
    }
}
