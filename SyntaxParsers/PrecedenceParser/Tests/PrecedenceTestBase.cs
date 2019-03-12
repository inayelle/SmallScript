using System.IO;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Tests
{
	public abstract class PrecedenceTestBase
	{
		protected static readonly string StaticFilesDir;

		static PrecedenceTestBase()
		{
			StaticFilesDir = Path.GetFullPath("../../../StaticFiles");
		}

		protected IGrammar GetGrammarFromFile(string path)
		{
			var parser = new BackusNaurGrammarParser(new PrecedenceEntryFactory());
			
			using (var file = new FileStream(path, FileMode.Open))
			{
				return parser.Parse(file);
			}
		}
	}
}