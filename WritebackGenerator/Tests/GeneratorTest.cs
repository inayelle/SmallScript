using System.Collections.Generic;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Details;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Base;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using SmallScript.SyntaxParsers.PrecedenceParser.Parser.Details;
using Xunit;
using Xunit.Abstractions;

namespace SmallScript.PolishWritebackGenerator.Tests
{
	public class GeneratorTest : SmallScriptTestBase
	{
		protected static string GrammarFile => "grammar";
		protected static string ExampleFile => "example.test";

		private readonly ITestOutputHelper _output;

		public GeneratorTest(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void TestGenerate()
		{
			var generator = CreateGenerator();

			var writebackTokens = generator.Generate(GetValidTokens());

			for (var i = 0; i < writebackTokens.Length; ++i)
			{
				var token = writebackTokens[i];
				_output.WriteLine($"[{i,-3}] [{token.Value,-10}] {token}");
			}
		}

		private WritebackGenerator.Generator.Details.WritebackGenerator CreateGenerator()
		{
			return new WritebackGenerator.Generator.Details.WritebackGenerator();
		}

		private IEnumerable<IToken> GetValidTokens()
		{
			IGrammar grammar;

			using (var file = GetStaticFileStream(GrammarFile))
			{
				var parser = new BackusNaurGrammarParser(new PrecedenceEntryFactory());

				grammar = parser.Parse(file);
			}

			var input = GetStaticFileContents(ExampleFile);

			var lexicalParser = new RegexParser(grammar);
			lexicalParser.OnSuccessfulParse += PostParseFormattingHandler.Handle;

			var syntaxParser = new PrecedenceParser(grammar);

			var lexicalParseResult = lexicalParser.Parse(input);
			Assert.True(lexicalParseResult.Ok);

			var syntaxParseResult = syntaxParser.Parse(lexicalParseResult);
			Assert.True(syntaxParseResult.Ok);

			return lexicalParseResult.Tokens;
		}
	}
}