﻿using Microsoft.AspNetCore.Components.WebView.Maui;
using dotnet6_maui_blazor_hero_client.Data;

namespace dotnet6_maui_blazor_hero_client;

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
#endif

    builder.Services.AddSingleton<WeatherForecastService>();
    builder.Services.AddSingleton<HeroService>();

    return builder.Build();
	}
}
