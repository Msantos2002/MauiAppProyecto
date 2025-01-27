using MauiAppProyecto.MVVM.Models;
using MauiAppProyecto.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppProyecto.MVVM.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class ProductDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        Producto _producto = new();

        [ObservableProperty]
        int id;

        [ObservableProperty]
        int cantidad;

        public ObservableCollection<ImagenProducto> Imagenes { get; } = new();

        ProductosService _productosService;
        ComprasService _comprasService;
        public ProductDetailViewModel(ProductosService productosService, ComprasService comprasService)
        {
            _productosService = productosService;
            _comprasService = comprasService;

        }

        [RelayCommand]
        async Task ComprarAsync()
        {
            await _comprasService.Comprar(Cantidad, Producto.Id);
        }

        [RelayCommand]
        void Aumentar()
        {
            Cantidad += 1;
        }

        [RelayCommand]
        void Restar()
        {
            if (Cantidad > 1)
            {
                Cantidad -= 1;
            }
        }

        public async Task GetProductoAsync()
        {
            if (IsBusy)
                return;
            try
            {
                Cantidad = 1;
                IsBusy = true;
                Producto = await _productosService.GetProductoAsync(Id);
                if (Imagenes.Count > 0)
                    Imagenes.Clear();
                foreach (var imagen in Producto.Imagenes)
                {
                    Imagenes.Add(imagen);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                await Shell.Current.DisplayAlert("Error!", e.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}

