using System.IO;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Tests;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using Xunit;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Tests
{
	public class GeneratorTest : SmallScriptTestBase
	{
		private static readonly string GrammarFile;

		static GeneratorTest()
		{
			var staticDir = Path.GetFullPath("../../../StaticFiles");

			GrammarFile = Path.Combine(staticDir, "grammar");
		}
		
		private readonly Generator.Details.Generator _generator;

		public GeneratorTest()
		{
			_generator = new Generator.Details.Generator();
		}

		[Fact]
		public void TestGenerate()
		{
			var result = _generator.Generate(GetCorrectGrammar());
			
			Assert.True(result.Ok);
		}

		private static IGrammar GetCorrectGrammar()
		{
			var parser = new BackusNaurGrammarParser(new PrecedenceEntryFactory());
			
			using (var file = new FileStream(GrammarFile, FileMode.Open))
			{
				return parser.Parse(file);
			}
		}
	}
}