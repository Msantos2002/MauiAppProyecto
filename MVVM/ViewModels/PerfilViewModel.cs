using MauiAppProyecto.MVVM.Models;
using MauiAppProyecto.MVVM.Views;
using MauiAppProyecto.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace MauiAppProyecto.MVVM.ViewModels
{
    public partial class PerfilViewModel : BaseViewModel
    {
        [ObservableProperty]
        Usuario usuario;

        public ObservableCollection<Compra> Compras { get; } = new();

        AuthService _authService;
        ComprasService _comprasService;
        public PerfilViewModel(AuthService authService, ComprasService comprasService)
        {
            _authService = authService;
            _comprasService = comprasService;
        }

        [RelayCommand]
        async Task Logout()
        {
            await _authService.Logout();
        }

        public async Task UpdateUser()
        {
            Usuario = App.AppUser;
            if (Compras.Count > 0)
                Compras.Clear();
            var compras = await _comprasService.GetComprasAsync();
            foreach (var compra in compras)
            {
                Compras.Add(compra);
            }
        }
    }
}
