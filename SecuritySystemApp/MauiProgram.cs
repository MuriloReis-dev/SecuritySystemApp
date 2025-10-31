using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using LiveChartsCore.SkiaSharpView.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Plugin.FirebasePushNotifications;

namespace SecuritySystemApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseLiveCharts()
			.UseSkiaSharp()
			.UseMauiCommunityToolkit()
			.UseFirebasePushNotifications()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
	
}
