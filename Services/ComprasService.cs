using MauiAppProyecto.MVVM.Models;
using MauiAppProyecto.MVVM.Models.Responses;
using MauiAppProyecto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppProyecto.Services
{
    public class ComprasService
    {
        HttpClient _httpClient;
        JsonSerializerOptions _jsonSerializerOptions;
        ApiResponse _apiResponse;

        public ComprasService()
        {
            _httpClient = new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<List<Compra>> GetComprasAsync()
        {
            List<Compra> compras = new();
            string url = $"{APISetting.GetBaseURL()}Compras?IdUsuario={App.AppUser.Id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    _apiResponse = await JsonSerializer.DeserializeAsync<ApiResponse>(responseStream, _jsonSerializerOptions);
                    compras = JsonSerializer.Deserialize<List<Compra>>(_apiResponse.Result.ToString(), _jsonSerializerOptions);
                }
            }
            return compras;
        }

        public async Task Comprar(int n, int idP)
        {
            string url = $"{APISetting.GetBaseURL()}Compras";
            var postData = new
            {
                idUsuario = App.AppUser.Id,
                idProducto = idP,
                cantidad = n,

            };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                _apiResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<ApiResponse>(responseStream, _jsonSerializerOptions);


            }
            if (!_apiResponse.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Error", _apiResponse.ErrorMessages[0], "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Exito", "Compra Realizada", "OK");
            }
        }
    }
}
