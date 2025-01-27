using MauiAppProyecto.MVVM.Models;
using MauiAppProyecto.MVVM.Models.Responses;
using MauiAppProyecto.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MauiAppProyecto.Services
{
    public class ProductosService
    {
        HttpClient _httpClient;
        JsonSerializerOptions _jsonSerializerOptions;
        ApiResponse _apiResponse;
        public ProductosService()
        {
            _httpClient = new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<Producto> GetProductoAsync(int id)
        {
            Producto producto = new();
            string url = $"{APISetting.GetBaseURL()}Productos/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    _apiResponse = await JsonSerializer.DeserializeAsync<ApiResponse>(responseStream, _jsonSerializerOptions);
                    producto = JsonSerializer.Deserialize<Producto>(_apiResponse.Result.ToString(), _jsonSerializerOptions);
                }



            }
            else
            {
                if (_apiResponse.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    await Shell.Current.DisplayAlert("Error", "Producto no encontrado", "ok");
                    await Shell.Current.GoToAsync(nameof(ProductsView));
                }
            }


            return producto;
        }
        public async Task<List<Producto>> GetProductosAsync(int page = 1, int PageSize = 30)
        {
            var Productos = new List<Producto>();
            string url = $"{APISetting.GetBaseURL()}Productos?page={page}&pageSize={PageSize}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    _apiResponse = await JsonSerializer.DeserializeAsync<ApiResponse>(responseStream, _jsonSerializerOptions);
                    Productos = JsonSerializer.Deserialize<List<Producto>>(_apiResponse.Result.ToString(), _jsonSerializerOptions);
                }
            }
            return Productos;
        }
    }
}