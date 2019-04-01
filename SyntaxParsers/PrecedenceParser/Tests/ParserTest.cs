using System.IO;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Details;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.Shared.Base;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using Xunit;
using Xunit.Abstractions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Tests
{
	public class ParserTest : SmallScriptTestBase
	{
		private static readonly string   GrammarFile;
		private static readonly string   CorrectTestFile;
		private static readonly string   InvalidTestFile;
		private static readonly IGrammar Grammar;

		private readonly ITestOutputHelper               _testOutputHelper;
		private readonly Parser.Details.PrecedenceParser _parser;

		static ParserTest()
		{
			var staticDir = Path.GetFullPath("../../../StaticFiles");

			GrammarFile     = Path.Combine(staticDir, "grammar");
			CorrectTestFile = Path.Combine(staticDir, "example");
			InvalidTestFile = Path.Combine(staticDir, "invalid");

			Grammar = GetCorrectGrammar();
		}

		private static IGrammar GetCorrectGrammar()
		{
			var parser = new BackusNaurGrammarParser(new PrecedenceEntryFactory());

			using (var file = new FileStream(GrammarFile, FileMode.Open))
			{
				return parser.Parse(file);
			}
		}

		public ParserTest(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;

			_parser    = new Parser.Details.PrecedenceParser(Grammar);
		}

		[Fact]
		public void TestCorrectParse()
		{
			var lexResult = GetTestLexicalParseResult(CorrectTestFile);

			_parser.OnSequenceReplacement += s => _testOutputHelper.WriteLine(s);
			var result = _parser.Parse(lexResult);

			Assert.True(result.Ok);
			Assert.Null(result.Error);
		}

		[Fact]
		public void TestInvalidSyntax()
		{
			var lexResult = GetTestLexicalParseResult(InvalidTestFile);

			var result = _parser.Parse(lexResult);

			Assert.False(result.Ok);
			Assert.NotNull(result.Error);
			Assert.Equal(5, result.Error.OccuredAt.Line);
		}

		private LexicalParseResult GetTestLexicalParseResult(string filepath)
		{
			var parser = new RegexParser(Grammar);
			parser.OnSuccessfulParse += PostParseFormattingHandler.Handle;

			using (var file = new FileStream(filepath, FileMode.Open))
			{
				return parser.Parse(file);
			}
		}
	}
}