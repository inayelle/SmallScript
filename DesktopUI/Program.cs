using Avalonia;
using Avalonia.Logging.Serilog;

namespace SmallScript.DesktopUI
{
	class Program
	{
		static void Main()
		{
			BuildAvaloniaApp().Start<MainWindow>();
		}

		public static AppBuilder BuildAvaloniaApp()
			=> AppBuilder.Configure<App>()
			             .UsePlatformDetect()
			             .LogToDebug();
	}
}