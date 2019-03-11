using System.IO;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Details.Internal;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Tests;
using Xunit;

namespace SmallScript.LexicalParsers.RegexParser.Tests
{
	public class TokenFactoryTest : SmallScriptTestBase
	{
		static TokenFactoryTest()
		{
			var staticFilesDir = Path.GetFullPath("../../../StaticFiles");

			GrammarFile = Path.Combine(staticFilesDir, "grammar");
		}

		public TokenFactoryTest()
		{
			_factory = new TokenFactory(GetGrammar(), new IdentitySource());
		}

		private static readonly string GrammarFile;

		private readonly TokenFactory _factory;

		private static IGrammar GetGrammar()
		{
			var parser = new BackusNaurGrammarParser();

			using (var file = new FileStream(GrammarFile, FileMode.Open))
			{
				return parser.Parse(file);
			}
		}

		[Fact]
		public void TestCreateEolDelimiter()
		{
			var value = "\n";

			var token = _factory.Create(value, new FilePosition(1, 1));

			Assert.IsType<DelimiterToken>(token);
			Assert.Equal("<EOL>", token.Value);
		}

		[Fact]
		public void TestCreateVariable()
		{
			var value = "$var";

			var token = _factory.Create(value, new FilePosition(1, 1));

			Assert.IsType<VariableToken>(token);
			Assert.Equal("var", token.Value);
		}
	}
}