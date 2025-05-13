using Microsoft.Extensions.Logging;
using Twothousandfortyeight.Game;
using Twothousandfortyeight.Maui.KeyHandler;

namespace Twothousandfortyeight.Maui;

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
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Services.AddTransient<IKeyHandler, KeyHandler.KeyHandler>();

        builder.Services.Add2048GameServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}