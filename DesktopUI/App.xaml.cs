using Avalonia;
using Avalonia.Markup.Xaml;

namespace SmallScript.DesktopUI
{
	public class App : Application
	{
		public override void Initialize()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}