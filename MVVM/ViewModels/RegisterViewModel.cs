using MauiAppProyecto.MVVM.Models;
using MauiAppProyecto.MVVM.Views;
using MauiAppProyecto.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MauiAppProyecto.MVVM.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        AuthService _authService;
        [ObservableProperty]
        string nombre;
        [ObservableProperty]
        string password;

        public RegisterViewModel(AuthService authService)
        {
            _authService = authService;

        }

        [ICommand]
        async Task RegisterAsync()
        {
            if (Password is null)
            {
                await Shell.Current.DisplayAlert("Error", "Ingresa un valor en contraseña", "Ok");
                return;
            }
            if (Nombre is null)
            {
                await Shell.Current.DisplayAlert("Error", "Ingresa un valor en nombre", "Ok");
                return;
            }
            await _authService.Register(Nombre, Password);

        }

        [ICommand]
        async Task GoToLoginPageAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}
