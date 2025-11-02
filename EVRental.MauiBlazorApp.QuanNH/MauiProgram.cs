using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace EVRental.MauiBlazorApp.QuanNH
{
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

                builder.Services.AddScoped(sp =>
                {
    #if ANDROID
                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, certificate, chain, errors) => true
                    };
                    var baseAddress = new Uri("https://10.0.2.2:7021/");
                    return new HttpClient(handler) { BaseAddress = baseAddress };
    #else
                    var baseAddress = new Uri("https://localhost:7021/");
                    return new HttpClient { BaseAddress = baseAddress };
    #endif
                });

            return builder.Build();
        }
    }
}
