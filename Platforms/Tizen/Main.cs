using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace dotnet6_maui_blazor_hero_client;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
