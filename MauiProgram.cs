using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps() // THÊM DÒNG NÀY để dùng Maps
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Register app services and viewmodels for DI
            builder.Services.AddSingleton<Services.LocationService>();
            builder.Services.AddSingleton<Services.AudioService>();
            builder.Services.AddSingleton<Services.GeofenceService>();
            builder.Services.AddSingleton<ViewModels.MapViewModel>();
            builder.Services.AddTransient<Views.MapPage>();

            return builder.Build();
        }
    }
}