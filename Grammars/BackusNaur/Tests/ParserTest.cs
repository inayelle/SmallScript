using System.IO;
using SmallScript.Grammars.BackusNaur.Grammar;
using SmallScript.Grammars.BackusNaur.Parser;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.BackusNaur.Parser.Exceptions;
using SmallScript.Grammars.Shared.Exceptions;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Tests;
using Xunit;

namespace SmallScript.Grammars.BackusNaur.Tests
{
	public class ParserTest : SmallScriptTestBase
	{
		private static readonly string TestFile;

		static ParserTest()
		{
			TestFile = Path.Combine("../../..", "StaticFiles", "grammar");
		}

		private readonly BackusNaurGrammarParser _parser;

		public ParserTest()
		{
			_parser = new BackusNaurGrammarParser();
		}

		[Fact]
		public void TestParseWithString()
		{
			const string input = "<SYNTAX>	::= a b <C>\n" +
			                     "<C>		::= d e <F>\n" +
			                     "<F>		::= q w e";

			var grammar = _parser.Parse(input);

			IsType<BackusNaurGrammar>(grammar);

			Equal(3, grammar.Rules.Count);
			Equal(9, grammar.Entries.Count);
		}

		[Fact]
		public void TestParseWithEmptyString()
		{
			const string input = "";

			var grammar = _parser.Parse(input);

			IsType<BackusNaurGrammar>(grammar);

			Equal(0, grammar.Rules.Count);
			Equal(0, grammar.Entries.Count);
		}

		[Fact]
		public void TestParseWithInvalidString()
		{
			const string input = "<SYNTAX>  = a b <C>\n" +
			                     "<C>		::= d e <F>\n" +
			                     "<F>		::= q w e";

			Throws<GrammarParseException>(() => _parser.Parse(input));
		}

		[Fact]
		public void TestParseWithComments()
		{
			const string input = "<SYNTAX>	::= a b <C>\n" +
			                     "<C>		::= d e <F>\n" +
			                     "#<F>		::= q w e";

			var grammar = _parser.Parse(input);

			IsType<BackusNaurGrammar>(grammar);

			Equal(2, grammar.Rules.Count);
			Equal(7, grammar.Entries.Count);
		}

		[Fact]
		public void TestParseFromFile()
		{
			IGrammar grammar = null;

			using (var file = new FileStream(TestFile, FileMode.Open))
			{
				grammar = _parser.Parse(file);
			}

			IsType<BackusNaurGrammar>(grammar);
		}
	}
}