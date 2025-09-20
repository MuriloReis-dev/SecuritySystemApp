using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;

namespace SecuritySystemApp.Views;

[QueryProperty(nameof(Alarme), "Alarme")]
public partial class AlarmePage : ContentPage
{
    private AlarmeDTO? _alarme;
    public AlarmeDTO? Alarme
    {
        get => _alarme;
        set
        {
            _alarme = value;
            BindingContext = value;
        }
    }

    private readonly AlarmeViewModel _viewModel;

    public AlarmePage()
    {
        InitializeComponent();

        _viewModel = new AlarmeViewModel();
    }

    public void OnAlarmAreaTapped(object sender, EventArgs e)
    {
        // Área para editar o nome do alarme (apenas proprietário)
        Console.WriteLine("Área do alarme tocada para editar o nome.");
    }

    public async void OnToggleAlarmClicked(object sender, EventArgs e)
    {
        if (Alarme != null && Alarme.Id != null)
            await _viewModel.AlarmeOnOffAsync(Alarme.Id, Alarme.Ligado);
        else
            Console.WriteLine("Id do Alarme não pode ser nulo.");
    }
}
