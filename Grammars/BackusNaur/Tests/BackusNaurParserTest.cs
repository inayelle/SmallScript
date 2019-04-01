using System.Collections.Generic;
using System.IO;
using System.Linq;
using SmallScript.Grammars.BackusNaur.Grammar.Details;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Exceptions;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Base;
using Xunit;

namespace SmallScript.Grammars.BackusNaur.Tests
{
	public class BackusNaurParserTest : SmallScriptTestBase
	{
		static BackusNaurParserTest()
		{
			RulesFile = Path.Combine("../../..", "StaticFiles", "grammar");
		}

		public BackusNaurParserTest()
		{
			_parser = new BackusNaurGrammarParser();
		}

		private static readonly string RulesFile;

		private readonly BackusNaurGrammarParser _parser;

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
		public void Test()
		{
			var first = new NonTerminal("qwe");
			var last  = new NonTerminal("qwe");

			var list = new List<NonTerminal> { first, last };

			var dist = list.Distinct().ToList();
		
			Assert.Single(dist);
		}
	}
}