using MauiAppProyecto.MVVM.ViewModels;


namespace MauiAppProyecto.MVVM.Views;

public partial class LoginPage : ContentPage
{
    LoginUserViewModel _viewModel;
    public LoginPage(LoginUserViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}