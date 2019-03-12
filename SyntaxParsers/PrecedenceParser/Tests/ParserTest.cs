using System;
using System.Collections.Generic;
using System.IO;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Details;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Tests;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using Xunit;
using Xunit.Abstractions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Tests
{
	public class ParserTest : SmallScriptTestBase
	{
		private readonly        ITestOutputHelper _testOutputHelper;
		private static readonly string            GrammarFile;
		private static readonly string            TestFile;

		private static readonly IGrammar _grammar;

		private readonly Parser.Details.PrecedenceParser _parser;

		static ParserTest()
		{
			var staticDir = Path.GetFullPath("../../../StaticFiles");

			GrammarFile = Path.Combine(staticDir, "grammar");
			TestFile    = Path.Combine(staticDir, "example");

			_grammar = GetCorrectGrammar();
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
			_parser           = new Parser.Details.PrecedenceParser(_grammar);
		}

		[Fact]
		public void TestParse()
		{
			var lexResult = GetTestLexicalParseResult();

			if (!lexResult.Ok)
			{
				throw new Exception("BAD EXAMPLE");
			}

			_parser.OnSequenceReplacement += s => _testOutputHelper.WriteLine(s);
			var result = _parser.Parse(lexResult);

			var message = result.Ok ? "OK" : $"{result.Error.Message} at {result.Error.OccuredAt}";
			_testOutputHelper.WriteLine(message);

			Assert.True(result.Ok);
			Assert.Null(result.Error);
		}

		private static LexicalParseResult GetTestLexicalParseResult()
		{
			var parser = new RegexParser(_grammar);

			using (var file = new FileStream(TestFile, FileMode.Open))
			{
				return parser.Parse(file);
			}
		}
	}
}