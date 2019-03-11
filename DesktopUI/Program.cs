using Avalonia;
using Avalonia.Logging.Serilog;

namespace SmallScript.DesktopUI
{
	internal class Program
	{
		private static void Main()
		{
			BuildAvaloniaApp().Start<MainWindow>();
		}

		public static AppBuilder BuildAvaloniaApp()
		{
			return AppBuilder.Configure<App>()
			                 .UsePlatformDetect()
			                 .LogToDebug();
		}
	}
}