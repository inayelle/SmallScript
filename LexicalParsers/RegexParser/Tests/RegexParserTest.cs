using System.IO;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.Shared.Tests;
using Xunit;

namespace SmallScript.LexicalParsers.RegexParser.Tests
{
	public class RegexParserTest : SmallScriptTestBase
	{
		static RegexParserTest()
		{
			var staticFilesDir = Path.GetFullPath("../../../StaticFiles");

			CorrectSyntaxFile = Path.Combine(staticFilesDir, "example");
			InvalidSyntaxFile = Path.Combine(staticFilesDir, "invalid");
			GrammarFile       = Path.Combine(staticFilesDir, "grammar");
		}

		public RegexParserTest()
		{
			_parser = new Parser.Details.RegexParser(GetGrammar());
		}

		private static readonly string GrammarFile;
		private static readonly string CorrectSyntaxFile;
		private static readonly string InvalidSyntaxFile;

		private readonly Parser.Details.RegexParser _parser;

		private static IGrammar GetGrammar()
		{
			var parser = new BackusNaurGrammarParser();

			using (var file = new FileStream(GrammarFile, FileMode.Open))
			{
				return parser.Parse(file);
			}
		}

		[Fact]
		public void TestParseWithCorrectSyntax()
		{
			LexicalParseResult result = null;

			using (var file = new FileStream(CorrectSyntaxFile, FileMode.Open))
			{
				result = _parser.Parse(file);
			}

			Assert.True(result.Ok);
			Assert.Null(result.Error);
			Assert.NotEmpty(result.Tokens);
			Assert.Equal(6, result.Constants.Count);
			Assert.Equal(3, result.Variables.Count);
		}

		[Fact]
		public void TestParseWithInvalidSyntax()
		{
			LexicalParseResult result = null;

			using (var file = new FileStream(InvalidSyntaxFile, FileMode.Open))
			{
				result = _parser.Parse(file);
			}

			Assert.False(result.Ok);
			Assert.NotNull(result.Error);
		}
	}
}