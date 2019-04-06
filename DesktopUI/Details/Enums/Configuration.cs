using System.IO;

namespace SmallScript.DesktopUI.Details.Enums
{
	internal static class Configuration
	{
		private static readonly string ConfigurationDirectory = "./Configuration";
		
		public static string GrammarFile { get; } = Path.Combine(ConfigurationDirectory, "grammar");
	}
}