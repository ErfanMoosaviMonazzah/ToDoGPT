using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ToDoGPT;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        string dbName = "ToDoGPT.db3";
        builder.Services.AddSingleton<DBMan>(s => ActivatorUtilities.CreateInstance<DBMan>(s, dbName));
        builder.Services.AddSingleton<APIMan>(s => ActivatorUtilities.CreateInstance<APIMan>(s));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
