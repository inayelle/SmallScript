using System;
using System.IO;
using System.Linq;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Base;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Tests
{
	public class GeneratorTest : SmallScriptTestBase
	{
		private readonly        ITestOutputHelper _testOutputHelper;
		private static readonly string            GrammarFile;
		private static readonly string            OutputFile;

		static GeneratorTest()
		{
			var staticDir = Path.GetFullPath("../../../StaticFiles");

			GrammarFile = Path.Combine(staticDir, "grammar");
			OutputFile  = Path.Combine(staticDir, "output");
		}

		private readonly Generator.Details.Generator _generator;

		public GeneratorTest(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
			_generator        = new Generator.Details.Generator();
		}

		[Fact]
		public void TestGenerate()
		{
			var result = _generator.Generate(GetCorrectGrammar());

			_testOutputHelper.WriteLine(result.Message);
			
			using (var file = new FileStream(OutputFile, FileMode.Create))
			using (var writer = new StreamWriter(file))
			{
				foreach (var pair in result.Pairs.Where(x=>x.IsFaulty()).OrderBy(x => x.ToString(), StringComparer.Ordinal))
				{
					_testOutputHelper.WriteLine(pair.ToString());
					writer.WriteLine(pair);
				}
			}

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