using MauiAppProyecto.MVVM.Models;
using MauiAppProyecto.Services;

namespace MauiAppProyecto;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;

    public MainPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            // Cambia "products" por el endpoint correspondiente de tu API
            var data = await _apiService.GetAsync<Product>("products");
            DataList.ItemsSource = data;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los datos: {ex.Message}", "OK");
        }
    }
}
