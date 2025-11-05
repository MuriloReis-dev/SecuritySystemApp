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

    // Modifique o evento OnPageSizeChanged para ser menos frequente
    private DateTime _lastResize = DateTime.MinValue;
    private const int RESIZE_DELAY_MS = 250; // Delay entre redraws

    /// <summary>
    /// Evento disparado ao mudar o tamanho da página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnPageSizeChanged(object? sender, EventArgs e)
    {
        try
        {
            // Evita múltiplos redraws em sequência
            if ((DateTime.Now - _lastResize).TotalMilliseconds < RESIZE_DELAY_MS)
                return;

            _lastResize = DateTime.Now;

            // Aguarda um momento para evitar múltiplos redraws
            await Task.Delay(RESIZE_DELAY_MS);

            var dados = await _viewModel.GerarDadosGrafico();
            if (dados != null && dados.Any())
            {
                MainThread.BeginInvokeOnMainThread(() => MontarGrafico(dados));
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

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            ClearOldChart(); // Limpa qualquer gráfico residual
            
            var dadosGrafico = await _viewModel.GerarDadosGrafico();
            if (dadosGrafico?.Any() == true)
            {
                MontarGrafico(dadosGrafico);
            }

            DadosAlarmes = await _viewModel.CarregarAlarmesAsync();
            AlarmesList.ItemsSource = DadosAlarmes;

            if (DadosAlarmes == null || DadosAlarmes.Count == 0)
                AlarmesList.IsVisible = false;
            else
                ListaVaziaLabel.IsVisible = false;
        });
    }

    private void MontarGrafico(List<EntradasDTO>? dadosGrafico)
    {
        if (dadosGrafico == null)
        return;

        // Esconde o container enquanto remontamos o gráfico (evita sobreposição visual)
        MainThread.BeginInvokeOnMainThread(() => 
        {
            ChartContainer.IsVisible = false;
            GraficoVazioLabel.IsVisible = false; // esconder label enquanto desenha
        });

        // Remove chart anterior ANTES de esconder o label
        ClearOldChart();
        
        GraficoVazioLabel.IsVisible = false;

        // Cria nova instância do CartesianChart com todas as propriedades definidas ANTES de adicionar ao container
        _chartInstance = new LiveChartsCore.SkiaSharpView.Maui.CartesianChart
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
            Series = new ISeries[] { /* será preenchido abaixo */ },
            XAxes = Array.Empty<Axis>(),
            YAxes = Array.Empty<Axis>(),
        };

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

        // Adiciona o chart ao container POR ÚLTIMO e torna visível novamente
        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                ChartContainer.Children.Clear();
                ChartContainer.Children.Add(_chartInstance);
                ChartContainer.IsVisible = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar chart: {ex}");
                // Em caso de falha, mostra label de gráfico vazio
                ChartContainer.IsVisible = false;
                GraficoVazioLabel.IsVisible = true;
            }
        });
    }

    private void ClearOldChart()
    {
        if (_chartInstance == null) return;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                // Primeiro remove do container
                if (ChartContainer.Children.Contains(_chartInstance))
                {
                    ChartContainer.Children.Clear(); // Limpa todos os filhos
                }

                // Tenta limpar recursos
                if (_chartInstance is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                // Limpa a referência
                _chartInstance = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao limpar chart antigo: {ex}");
            }
        });
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
