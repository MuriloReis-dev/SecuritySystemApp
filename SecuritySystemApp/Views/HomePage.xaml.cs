using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Interfaces;
using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Measure;

namespace SecuritySystemApp.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;
    private readonly INavigationService _navigationService;

    private LiveChartsCore.SkiaSharpView.Maui.CartesianChart? _chartInstance;

    public List<AlarmeDTO>? DadosAlarmes;
    public HomePage()
    {
        InitializeComponent();
        _viewModel = new HomeViewModel();
        BindingContext = _viewModel;

        _navigationService = new NavigationService();
        this.SizeChanged += OnPageSizeChanged;
    }

    /// <summary>
    /// Evento disparado ao mudar o tamanho da página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnPageSizeChanged(object? sender, EventArgs e)
    {
        // Reconstrói o gráfico ao mudar de tamanho para evitar sobreposição
        try
        {
            var dados = _viewModel.GerarDadosGrafico();
            if (dados != null && dados.Any())
            {
                MontarGrafico(dados);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao tratar SizeChanged: {ex}");
        }
    }

    /// <summary>
    /// Carrega os dados dos alarmes ao aparecer a página
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var dadosGrafico = _viewModel.GerarDadosGrafico();
        MontarGrafico(dadosGrafico);

        DadosAlarmes = await _viewModel.CarregarAlarmesAsync();
        AlarmesList.ItemsSource = DadosAlarmes;

        // Ajusta visibilidade dos elementos com base nos dados carregados
        if (DadosAlarmes == null || DadosAlarmes.Count == 0)
            AlarmesList.IsVisible = false;
        else
            ListaVaziaLabel.IsVisible = false;
    }

    private void MontarGrafico(List<EntradasDTO> dadosGrafico)
    {
        if (dadosGrafico == null || dadosGrafico.Count() == 0)
            return;

        // Remove chart anterior se existir (evita sobreposição)
        ClearOldChart();

        // Cria nova instância do CartesianChart
        _chartInstance = new LiveChartsCore.SkiaSharpView.Maui.CartesianChart
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent
        };

        // Adiciona o chart ao container
        ChartContainer.Children.Add(_chartInstance);

        // Informações sobre a semana atual
        DateTime hoje = DateTime.Now; // Hoje
        DateTime inicioSemana = hoje.AddDays(-(int)hoje.DayOfWeek); // Domingo
        DateTime fimSemana = inicioSemana.AddDays(6); // Sábado

        var diasSemana = Enumerable.Range(0, 7)
            .Select(i => inicioSemana.AddDays(i))
            .ToList();

        var valores = diasSemana.Select(dia =>
        {
            var dado = dadosGrafico.FirstOrDefault(x => x.Data.Date == dia.Date);
            if (dado != null && dia.Date <= hoje)
                return (float)dado.QtdEntradas;
            else
                return 0.0f;
        }).ToList();

        // Define rótulos (X) e valores (Y)
        var labels = diasSemana.Select(d => d.ToString("ddd")[..1].ToUpper()).ToList();

        // Define as séries (barras)
        _chartInstance.Series = new ISeries[]
        {
            new ColumnSeries<float>
            {
                Values = valores,
                Fill = new SolidColorPaint(SKColors.DeepSkyBlue.WithAlpha(180)),
                Stroke = null,
                MaxBarWidth = 15,
                Rx = 10,
                Ry = 10
            }
        };

        // Eixo X (dias)
        _chartInstance.XAxes = new[]
        {
            new Axis
            {
                Labels = labels,
                LabelsRotation = 0,
                TextSize = 16,
                SeparatorsPaint = new SolidColorPaint(SKColors.LightGray) { StrokeThickness = 1 },
                ShowSeparatorLines = false
            }
        };

        // Eixo Y (Valores)
        _chartInstance.YAxes = new[]
        {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = Math.Ceiling(valores.Max() / 5) * 5,
                TextSize = 14,
                SeparatorsPaint = new SolidColorPaint(SKColors.LightGray) { StrokeThickness = 1 },
                ShowSeparatorLines = true
            }
        };

        // Aparência geral
        _chartInstance.DrawMarginFrame = new DrawMarginFrame
        {
            Fill = new SolidColorPaint(SKColors.Transparent),
            Stroke = new SolidColorPaint(SKColors.Gray.WithAlpha(80))
        };

        // Desativar Tooltips
        _chartInstance.TooltipPosition = TooltipPosition.Hidden;
    }

    private void ClearOldChart()
    {
        try
        {
            if (_chartInstance != null)
            {
                // Tenta esconder tooltip caso esteja aberto
                try
                {
                    // A API exige que se passe a instância do Chart ao chamar Hide
                    var core = _chartInstance.CoreChart;
                    if (core != null)
                    {
                        core.Tooltip?.Hide(core);
                    }
                }
                catch { /* Ignore se API não expor Hide/assinar diferente */ }

                // Remove do container e dispose se possível
                if (ChartContainer.Children.Contains(_chartInstance))
                    ChartContainer.Children.Remove(_chartInstance);

                // Some implementations may expose Dispose, tentar limpar referência
                _chartInstance = null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao limpar chart antigo: {ex}");
        }
    }

    /// <summary>
    /// Evento ao tocar em um alarme para ver detalhes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnAlarmeTapped(object sender, TappedEventArgs e)
    {
        int alarmeId = e.Parameter == null ? 0 : (int)e.Parameter;
        if (alarmeId != 0)
        {
            await _navigationService.NavegarAsync(nameof(AlarmePage), new Dictionary<string, object>
            {
                ["AlarmeId"] = alarmeId
            });
        }
    }
}
