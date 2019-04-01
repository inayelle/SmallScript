using System.Collections.Generic;
using System.IO;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Details;
using SmallScript.LexicalParsers.Shared.Extensions;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Base;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using SmallScript.SyntaxParsers.PrecedenceParser.Parser.Details;
using Xunit;

namespace SmallScript.Calculator.Tests
{
	public class CalculatorTest : SmallScriptTestBase
	{
		private static readonly string GrammarFile;

		static CalculatorTest()
		{
			GrammarFile = Path.Combine("../../../StaticFiles", "calculator");
		}

		[Fact]
		public void TestExpression()
		{
			var grammar = GetGrammarFromFile(GrammarFile);

			const string input          = "( 10 - 1 ) * ( 5 + ( 1 + 3 ) / 2 ) ** 2 / 3 + 100";
			const double expectedResult = 247;

			var tokenizer    = new RegexParser(grammar);
			var syntaxParser = new PrecedenceParser(grammar);
			
			var tokenizerResult = tokenizer.Parse(input);
			Assert.True(tokenizerResult.Ok);

			var syntaxResult = syntaxParser.Parse(tokenizerResult);
			Assert.True(syntaxResult.Ok);

			var calculator = new Calculator.Details.Calculator();

			var result = calculator.Evaluate(tokenizerResult.Tokens);
			Assert.Equal(expectedResult, result);
		}

		private static IGrammar GetGrammarFromFile(string filepath)
		{
			using (var file = new FileStream(filepath, FileMode.Open))
			{
				var parser = new BackusNaurGrammarParser(new PrecedenceEntryFactory());

				return parser.Parse(file);
			}
		}
	}
}