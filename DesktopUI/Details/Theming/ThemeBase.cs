using Avalonia.Controls;
using Avalonia.Media;

namespace SmallScript.DesktopUI.Details.Theming
{
	internal abstract class ThemeBase
	{
		public IBrush BackgroundColor  { get; }
		public IBrush ControlsColor    { get; }
		public IBrush ControlsMidColor { get; }
		public IBrush ForegroundColor  { get; }
		public IBrush BorderColor      { get; }

		protected ThemeBase(string background, string controls, string controlsMid, string foreground, string border)
		{
			BackgroundColor  = Brush.Parse(background);
			ControlsColor    = Brush.Parse(controls);
			ForegroundColor  = Brush.Parse(foreground);
			ControlsMidColor = Brush.Parse(controlsMid);
			BorderColor      = Brush.Parse(border);
		}

		public void Apply(Window window)
		{
			window.Resources["ThemeBackgroundBrush"] = BackgroundColor;
			window.Resources["ThemeControlBrush"]    = ControlsColor;
			window.Resources["ThemeControlMidBrush"] = ControlsMidColor;
			window.Resources["ThemeForegroundBrush"] = ForegroundColor;
			window.Resources["ThemeBorderBrush"]     = BorderColor;
		}
	}
}