namespace SecuritySystemApp.Models;

public class Alarme
{
    public int id_status { get; set; }
    public bool ligado { get; set; }
    public DateTime data_hora { get; set; }
    public string? permitidos { get; set; }
    public int id_usuario { get; set; }
}
