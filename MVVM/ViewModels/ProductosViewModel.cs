using MauiAppProyecto.MVVM.Models;
using MauiAppProyecto.MVVM.Views;
using MauiAppProyecto.Services;
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
    public partial class ProductosViewModel : BaseViewModel
    {
        ProductosService _productosService;
        public ObservableCollection<Producto> Productos { get; } = new();
        public ProductosViewModel(ProductosService productosService)
        {
            _productosService = productosService;
            Tittle = "Productos";
        }
        [ICommand]
        async Task GoToDetailsAsync(int id)
        {
            if (id == 0)
                return;
            await Shell.Current.GoToAsync($"{nameof(ProductDetails)}?id={id}", true);
        }
        public async Task GetProductosAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var productos = await _productosService.GetProductosAsync();

                if (Productos.Count != 0)
                    Productos.Clear();
                foreach (var p in productos)
                    Productos.Add(p);
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

