using SecuritySystemApp.Services;
using SecuritySystemApp.Interfaces;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;
// Teste de gráfico para HomePage
using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Measure;

namespace SecuritySystemApp.Views;

public partial class MainPage : ContentPage
{
    // No futuro, mudar a MainPage para uma tela de carregamento e alterar o código para redirecionar para a tela de Login (ou para Home caso esteja logado)
    private readonly MainViewModel _viewModel;

    private readonly INavigationService _navigationService;
    private readonly ApiService _apiService;
    private readonly AuthService _authService;

    // Chart instance created/destroyed dynamically to avoid overlapping renders
    private LiveChartsCore.SkiaSharpView.Maui.CartesianChart? _chartInstance;
    // O XAML já gera um campo para o elemento com x:Name="ChartContainer".
    // Não declarar outra propriedade com o mesmo nome evita ambiguidade de símbolos.

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        _navigationService = new NavigationService();
        _apiService = new ApiService();
        _authService = new AuthService();

        CadrastroBtn.Clicked += OnCadrastroBtnClicked;
        LoginBtn.Clicked += OnLoginBtnClicked;
        HomeBtn.Clicked += OnHomeBtnClicked;
        CadastroPessoaBtn.Clicked += OnCadastroPessoaBtnClicked;
        Appearing += OnAppearing;

        // Vamos reagir a mudanças de tamanho criando/atualizando o chart de forma segura
        this.SizeChanged += OnPageSizeChanged;
    }

    // Evento disparado quando a página aparece
    private async void OnAppearing(object? sender, EventArgs e)
    {
        base.OnAppearing();

        // Colocar aqui todo o código executado ao abrir o app
        Console.WriteLine($"UserID: {Preferences.Get("UserId", string.Empty)}");
        Console.WriteLine($"UserName: {Preferences.Get("UserName", string.Empty)}");
        Console.WriteLine($"UserEmail: {Preferences.Get("UserEmail", string.Empty)}");
        Console.WriteLine($"AuthToken: {Preferences.Get("AuthToken", string.Empty)}");

        bool tokenValido = await _authService.ValidateLoginAsync();
        Console.WriteLine($"Validação do token de usuário: {tokenValido}");

        // Teste de gráfico para HomePage
        var dados = _viewModel.GerarDadosGrafico();
        MontarGrafico(dados);
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

        // Define rótulos (X) e valores (Y)
        var labels = dadosGrafico.Select(d => d.Data.ToString("ddd")).ToList();
        var valores = dadosGrafico.Select(d => (float)d.QtdEntradas).ToList();

        // Define as séries (barras)
        _chartInstance.Series = new ISeries[]
        {
            new ColumnSeries<float>
            {
                Values = valores,
                Fill = new SolidColorPaint(SKColors.DeepSkyBlue.WithAlpha(180)),
                Stroke = null,
                MaxBarWidth = 30
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
                Name = "Dias da Semana",
                NameTextSize = 18,
                NamePadding = new LiveChartsCore.Drawing.Padding(0, 20, 0, 0),
                SeparatorsPaint = new SolidColorPaint(SKColors.LightGray) { StrokeThickness = 1 }
            }
        };

        // Eixo Y (Valores)
        _chartInstance.YAxes = new[]
        {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = 15,
                TextSize = 14,
                Name = "Qtd de Entradas",
                NameTextSize = 18,
                NamePadding = new LiveChartsCore.Drawing.Padding(0, 0, 0, 20),
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

    private async void OnCadrastroBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(CadastroPage));
    }

    private async void OnLoginBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(LoginPage));
    }

    private async void OnHomeBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(HomePage));
    }

    private async void OnCadastroPessoaBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(CadastroPessoaPage));
    }
}
