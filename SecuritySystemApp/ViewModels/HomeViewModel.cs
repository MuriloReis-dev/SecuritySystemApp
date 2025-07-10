using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using SecuritySystemApp.Models;
using SecuritySystemApp.Services;

namespace SecuritySystemApp.ViewModels;

public class HomeViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Alarme> Alarmes { get; set; } = new ObservableCollection<Alarme>();

    public async Task CarregarAlarmesAsync()
    {
        var service = new LeituraDBService();
        var dados = await service.CarregarAsync<Alarme>();
        Alarmes.Clear();
        foreach (var alarme in dados)
        {
            Alarmes.Add(alarme);
        }
        OnPropertyChanged(nameof(Alarmes)); // Atualiza página ao alterar conteúdo de Alarme
    }

    // Avisa a página para atualizar o BindingContext
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}