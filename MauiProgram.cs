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
        builder.Services.AddSingleton<Calculator>();
        builder.Services.AddSingleton<DbOps>();
        builder.Services.AddSingleton<LivestockPageVM>();
        builder.Services.AddSingleton<Livestock>();
        builder.Services.AddSingleton<AddAnimalPageVM>();
        builder.Services.AddTransient<UpdateAnimalPageVM>();
        builder.Services.AddTransient<UpdateAnimalPage>();
        builder.Services.AddSingleton<QueryPageVM>();
        builder.Services.AddSingleton<QueryPage>();
        builder.Services.AddSingleton<DashboardVM>();
        builder.Services.AddSingleton<DashBoard>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
