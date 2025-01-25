using MauiAppProyecto.MVVM.ViewModels;


namespace MauiAppProyecto.MVVM.Views;

public partial class ProductDetails : ContentPage
{
    ProductDetailViewModel _viewModel;
    public ProductDetails(ProductDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetProductoAsync();
    }


}