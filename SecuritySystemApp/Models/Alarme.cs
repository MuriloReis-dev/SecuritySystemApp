namespace SecuritySystemApp.Models;
using System.ComponentModel;

public class Alarme : INotifyPropertyChanged
{
    public int id_status { get; set; }

    private bool _ligado;
    public bool ligado // get -> lê o valor de "_ligado" | set -> define "_ligado" com o novo valor e notifica ao mudar de valor
    {
        get => _ligado;
        set
        {
            if (_ligado != value)
            {
                _ligado = value;
                OnPropertyChanged(nameof(ligado)); // Notifica que o valor "ligado" mudou
                OnPropertyChanged(nameof(ImagemAlarmeSource)); // Notifica que a imagem mudou
            }
        }
    }

    public DateTime data_hora { get; set; }

    public string? permitidos { get; set; }

    public int id_usuario { get; set; }

    // Propriedade que retorna o caminho da imagem com base no valor de 'ligado'
    public string ImagemAlarmeSource => ligado ? "alarme_on.png" : "alarme_off.png";

    // Avisa a página para atualizar o BindingContext
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
