using System.IO;
using SmallScript.Grammars.BackusNaur.Grammar;
using SmallScript.Grammars.BackusNaur.Grammar.Details;
using SmallScript.Grammars.BackusNaur.Parser;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.BackusNaur.Parser.Exceptions;
using SmallScript.Grammars.Shared.Exceptions;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Tests;
using Xunit;

namespace SmallScript.Grammars.BackusNaur.Tests
{
	public class BackusNaurParserTest : SmallScriptTestBase
	{
		private static readonly string RulesFile;

		static BackusNaurParserTest()
		{
			RulesFile = Path.Combine("../../..", "StaticFiles", "grammar.rules");
		}

		private readonly BackusNaurGrammarParser _parser;

		public BackusNaurParserTest()
		{
			_parser = new BackusNaurGrammarParser();
		}

		[Fact]
		public void TestParseWithCorrectString()
		{
			const string input = "<SYNTAX>	::= a b <C>\n" +
			                     "<C>		::= d e <F>\n" +
			                     "<F>		::= q w e";

			var grammar = _parser.Parse(input);

			Assert.IsType<BackusNaurGrammar>(grammar);

			Assert.Equal(3, grammar.Rules.Count);
			Assert.Equal(9, grammar.Entries.Count);
		}

		[Fact]
		public void TestParseWithEmptyString()
		{
			const string input = "";

			var grammar = _parser.Parse(input);

			Assert.IsType<BackusNaurGrammar>(grammar);

			Assert.Equal(0, grammar.Rules.Count);
			Assert.Equal(0, grammar.Entries.Count);
		}

		[Fact]
		public void TestParseWithInvalidString()
		{
			const string input = "<SYNTAX>  = a b <C>\n" +
			                     "<C>		::= d e <F>\n" +
			                     "<F>		::= q w e";

			Assert.Throws<GrammarParseException>(() => _parser.Parse(input));
		}

		[Fact]
		public void TestParseWithComments()
		{
			const string input = "<SYNTAX>	::= a b <C>\n" +
			                     "<C>		::= d e <F>\n" +
			                     "#<F>		::= q w e";

			var grammar = _parser.Parse(input);

			Assert.IsType<BackusNaurGrammar>(grammar);

			Assert.Equal(2, grammar.Rules.Count);
			Assert.Equal(7, grammar.Entries.Count);
		}

		[Fact]
		public void TestParseFromFile()
		{
			IGrammar grammar = null;

			using (var file = new FileStream(RulesFile, FileMode.Open))
			{
				grammar = _parser.Parse(file);
			}

			Assert.IsType<BackusNaurGrammar>(grammar);
		}
	}
}