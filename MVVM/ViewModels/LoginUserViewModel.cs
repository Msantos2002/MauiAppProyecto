using System;
using System.Collections.Generic;
using System.Linq;
using MauiAppProyecto.MVVM.Views;
using MauiAppProyecto.Services;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MauiAppProyecto.MVVM.ViewModels
{
    public partial class LoginUserViewModel : BaseViewModel
    {
        AuthService _authService;
        [ObservableProperty]
        string nombre;
        [ObservableProperty]
        string password;

        public LoginUserViewModel(AuthService authService)
        {
            _authService = authService;

        }

        [RelayCommand]
        async Task LoginAsync()
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
            await _authService.Login(Nombre, Password);
        }

        [RelayCommand]
        async Task GoToRegisterAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterView)}");
        }

    }
}
