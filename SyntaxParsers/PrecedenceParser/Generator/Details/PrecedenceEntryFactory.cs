using System.Collections.Generic;
using System.Text.RegularExpressions;
using SmallScript.Grammars.BackusNaur.Parser.Interfaces;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details
{
	public class PrecedenceEntryFactory : IEntryFactory
	{
		private const string NonTerminalRegex = @"^<[A-z0-9\-_]+>$";

		private readonly IDictionary<string, IGrammarEntry> _cache;

		public PrecedenceEntryFactory()
		{
			_cache = new Dictionary<string, IGrammarEntry>();
		}

		public IGrammarEntry CreateEntry(string value)
		{
			if (_cache.ContainsKey(value))
				return _cache[value];

			IGrammarEntry instance = null;

			if (Regex.IsMatch(value, NonTerminalRegex))
				instance = CreateNonTerminal(value);
			else
				instance = CreateTerminal(value);

			return _cache[value] = instance;
		}

		private static NonTerminal CreateNonTerminal(string value)
		{
			return new DetailedNonTerminal(value);
		}

		private static Terminal CreateTerminal(string value)
		{
			return new Terminal(value);
		}
	}
}