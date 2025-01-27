using MauiAppProyecto.MVVM.Models;
using MauiAppProyecto.MVVM.Models.Responses;
using MauiAppProyecto.MVVM.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace MauiAppProyecto.Services
{
    public class AuthService
    {
        HttpClient _httpClient;
        JsonSerializerOptions _jsonSerializerOptions;
        ApiResponse _apiResponse;
        public bool IsLogedIn { get; set; }

        public AuthService()
        {
            _httpClient = new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            };
            IsLogedIn = false;
        }
        public bool IsAuthenticated()
        {
            return IsLogedIn;
        }

        public async Task Login(string usuario, string pass)
        {
            string url = $"{APISetting.GetBaseURL()}Usuarios/Login";
            var postData = new { nombre = usuario, password = pass };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    _apiResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<ApiResponse>(responseStream, _jsonSerializerOptions);
                    var loginResponse = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(_apiResponse.Result.ToString(), _jsonSerializerOptions);
                    var user = loginResponse.Usuario;
                    IsLogedIn = true;
                    App.AppUser = user;
                    await Shell.Current.Navigation.PopToRootAsync();
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Credenciales Invalidas", "Ok");
                IsLogedIn = false;
            }

        }
        public async Task Logout()
        {
            IsLogedIn = false;
            App.AppUser = null;
            await Shell.Current.GoToAsync($"{nameof(ProductsView)}");

        }

        public async Task Register(string usuario, string pass)
        {
            string url = $"{APISetting.GetBaseURL()}Usuarios/Registro";
            var postData = new { nombre = usuario, password = pass };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    _apiResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<ApiResponse>(responseStream, _jsonSerializerOptions);
                    var user = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_apiResponse.Result.ToString(), _jsonSerializerOptions);

                    IsLogedIn = true;
                    App.AppUser = user;
                    await Shell.Current.Navigation.PopToRootAsync();
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Ya existe un usuario con ese nombre", "Ok");
                IsLogedIn = false;
            }

        }
    }
}
