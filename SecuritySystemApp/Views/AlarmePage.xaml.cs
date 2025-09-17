using SecuritySystemApp.Models;

namespace SecuritySystemApp.Views;

[QueryProperty(nameof(Alarme), "Alarme")]
public partial class AlarmePage : ContentPage
{
    private Alarme? _alarme;
    public Alarme? Alarme
    {
        get => _alarme;
        set
        {
            _alarme = value;
            BindingContext = value;
        }
    }

    public AlarmePage()
    {
        InitializeComponent();
    }

    public void OnAlarmAreaTapped(object sender, EventArgs e)
    {
        // Área para editar o nome do alarme (apenas proprietário)
        Console.WriteLine("Área do alarme tocada para editar o nome.");
    }
}
