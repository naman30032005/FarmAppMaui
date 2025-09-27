using Farm.Utitlity;
using Farm.Views;
using Microsoft.Extensions.Logging;

namespace Farm;
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
        builder.Services.AddSingleton<DbOps>();
        builder.Services.AddSingleton<LivestockPageVM>();
        builder.Services.AddSingleton<Livestock>();
        builder.Services.AddSingleton<AddAnimalPageVM>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
