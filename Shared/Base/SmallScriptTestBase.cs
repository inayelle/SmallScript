using System.IO;
using System.Text;

namespace SmallScript.Shared.Base
{
	public abstract class SmallScriptTestBase
	{
		protected static readonly string StaticFilesDir;

		static SmallScriptTestBase()
		{
			StaticFilesDir = Path.GetFullPath("../../../StaticFiles");
		}

		protected static string GetStaticFileContents(string filename)
		{
			var path = Path.Combine(StaticFilesDir, filename);

			return File.ReadAllText(path, Encoding.UTF8);
		}
		
		protected static Stream GetStaticFileStream(string filename)
		{
			var path = Path.Combine(StaticFilesDir, filename);

			return new FileStream(path, FileMode.Open, FileAccess.Read);
		}
	}
}