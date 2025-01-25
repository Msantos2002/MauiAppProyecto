using MauiAppProyecto.MVVM.Models;
using MauiAppProyecto.MVVM.ViewModels;
namespace MauiAppProyecto.MVVM.Views;

public partial class PerfilView : ContentPage
{
    PerfilViewModel _viewModel;
    public PerfilView(PerfilViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.UpdateUser();
    }
}