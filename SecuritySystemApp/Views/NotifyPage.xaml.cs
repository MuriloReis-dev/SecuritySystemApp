using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Interfaces;
using System.Linq;
using System.Collections.ObjectModel;

namespace SecuritySystemApp.Views;

public partial class NotifyPage : ContentPage
{
    private readonly NotifyViewModel _viewModel;
    private readonly INavigationService _navigationService;

    public List<NotifyDTO>? Dados;
    public NotifyPage()
    {
        InitializeComponent();
        _viewModel = new NotifyViewModel();
        BindingContext = _viewModel;

        _navigationService = new NavigationService();
    }

    /// <summary>
    /// Carrega os dados dos alarmes ao aparecer a página
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Dados = await _viewModel.CarregarNotificacoesAsync();
        SetNotifications(Dados);
    }

    // Agrupa notificações em Hoje / Últimos 7 dias / Outras e atualiza os CollectionViews (se existirem)
    private void SetNotifications(IEnumerable<NotifyDTO>? notifications)
    {
        var list = notifications?.ToList() ?? new List<NotifyDTO>();

        DateTime hoje = DateTime.Now.Date;

        var hojeItems = list
            .Where(n => n.DataHora.Date == hoje)
            .OrderByDescending(n => n.DataHora)
            .ToList();

        var seteDiasItems = list
            .Where(n => n.DataHora.Date < hoje && (hoje - n.DataHora.Date).TotalDays <= 7)
            .OrderByDescending(n => n.DataHora)
            .ToList();

        var outrasItems = list
            .Where(n => (hoje - n.DataHora.Date).TotalDays > 7)
            .OrderByDescending(n => n.DataHora)
            .ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            // Se a XAML já tiver TodayList / Last7DaysList / OlderList, popula elas
            if (this.FindByName<CollectionView>("TodayList") != null)
            {
                this.FindByName<CollectionView>("TodayList").ItemsSource = new ObservableCollection<NotifyDTO>(hojeItems);
                this.FindByName<CollectionView>("Last7DaysList").ItemsSource = new ObservableCollection<NotifyDTO>(seteDiasItems);
                this.FindByName<CollectionView>("OlderList").ItemsSource = new ObservableCollection<NotifyDTO>(outrasItems);

                // controla visibilidade de uma label de lista vazia se existir
                var emptyLabel = this.FindByName<Label>("ListaVaziaLabel");
                if (emptyLabel != null)
                    emptyLabel.IsVisible = !(hojeItems.Any() || seteDiasItems.Any() || outrasItems.Any());
            }
            else if (this.FindByName<CollectionView>("NotifyList") != null)
            {
                // compatibilidade: se existe apenas NotifyList, mostra tudo junto
                this.FindByName<CollectionView>("NotifyList").ItemsSource = list;
                var emptyLabel = this.FindByName<Label>("ListaVaziaLabel");
                if (emptyLabel != null)
                    emptyLabel.IsVisible = !list.Any();
            }
        });
    }
}