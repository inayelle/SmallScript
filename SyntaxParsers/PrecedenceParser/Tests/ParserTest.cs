using System.IO;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Tests;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using Xunit;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Tests
{
	public class ParserTest : SmallScriptTestBase
	{
		private static readonly string GrammarFile;

		private readonly PrecedenceParser _parser;
		
		static ParserTest()
		{
			var staticDir = Path.GetFullPath("../../../StaticFiles");

			GrammarFile = Path.Combine(staticDir, "grammar");
		}
		
		private static IGrammar GetCorrectGrammar()
		{
			var parser = new BackusNaurGrammarParser(new PrecedenceEntryFactory());
			
			using (var file = new FileStream(GrammarFile, FileMode.Open))
			{
				return parser.Parse(file);
			}
		}
		
		[Fact]
		public void TestParse()
		{
			
		}
	}
}