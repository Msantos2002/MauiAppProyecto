using MauiAppProyecto.MVVM.ViewModels;
using MauiAppProyecto.Services;

namespace MauiAppProyecto.MVVM.Views;

public partial class ProductsView : ContentPage
{
    private readonly ProductosViewModel _viewModel;
    private readonly AuthService _authService;
    public ProductsView(ProductosViewModel viewModel, AuthService authService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _authService = authService;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.GetProductosAsync();

    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (!_authService.IsAuthenticated())
        {
            // User is logged in, navigate to MainPage with tab bar
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }




}