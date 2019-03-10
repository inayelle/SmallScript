using System.IO;
using Moq;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Details;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Tests;
using Xunit;

namespace SmallScript.LexicalParsers.RegexParser.Tests
{
	public class RegexParserTest : SmallScriptTestBase
	{
		private static readonly string GrammarFile;
		private static readonly string CorrectSyntaxFile;
		private static readonly string InvalidSyntaxFile;

		static RegexParserTest()
		{
			var staticFilesDir = Path.GetFullPath("../../../StaticFiles");

			CorrectSyntaxFile = Path.Combine(staticFilesDir, "correct");
			InvalidSyntaxFile = Path.Combine(staticFilesDir, "invalid");
			GrammarFile       = Path.Combine(staticFilesDir, "grammar");
		}

		private readonly Parser.Details.RegexParser _parser;

		public RegexParserTest()
		{
			_parser = new Parser.Details.RegexParser(GetGrammar());
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
			Assert.NotEmpty(result.Constants);
			Assert.NotEmpty(result.Variables);
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

		private static IGrammar GetGrammar()
		{
			var parser = new BackusNaurGrammarParser();

			using (var file = new FileStream(GrammarFile, FileMode.Open))
			{
				return parser.Parse(file);
			}
		}
	}
}