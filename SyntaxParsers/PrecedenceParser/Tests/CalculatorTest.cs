using System.IO;
using SmallScript.LexicalParsers.RegexParser.Parser.Details;
using Xunit;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Tests
{
	public class CalculatorTest : PrecedenceTestBase
	{
		private static readonly string GrammarFile;

		static CalculatorTest()
		{
			GrammarFile = Path.Combine(StaticFilesDir, "calculator");
		}

		[Fact]
		public void TestExpression()
		{
			var grammar = GetGrammarFromFile(GrammarFile);

			const string input = "( 10 - 1 ) * ( 5 + ( 1 + 3 ) / 2 ) ** 2 / 3 + 100";
			const double expectedResult = 247;

			var tokenizer = new RegexParser(grammar);
			var syntaxParser = new Parser.Details.PrecedenceParser(grammar);
			
			var tokenizerResult = tokenizer.Parse(input);
			Assert.True(tokenizerResult.Ok);

			var syntaxResult = syntaxParser.Parse(tokenizerResult);
			Assert.True(syntaxResult.Ok);

			var calculator = new Calculator.Details.Calculator();

			var result = calculator.Evaluate(tokenizerResult.Tokens);
			Assert.Equal(expectedResult, result);
		}
	}
}