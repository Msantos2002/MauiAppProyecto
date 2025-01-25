
using Microsoft.Extensions.Logging;
using System.Net.Http;
using MauiAppProyecto.Services;
namespace MauiAppProyecto;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Registrar HttpClient con la URL base de la API
        builder.Services.AddHttpClient("ApiClient", client =>
        {
            client.BaseAddress = new System.Uri("https://localhost:7181"); // URL base de tu API
        });
        // Registrar el servicio de la API
        builder.Services.AddTransient<ApiService>();

        return builder.Build();
    }
}