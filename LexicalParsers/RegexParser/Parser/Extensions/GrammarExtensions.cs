using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.Shared.Extensions;

namespace SmallScript.LexicalParsers.RegexParser.Parser.Extensions
{
	internal static class GrammarExtensions
	{
		public static IGrammarEntry GetEntryByValue(this IGrammar grammar, string value)
		{
			return grammar.Entries.FirstOrDefault(e => e.Value.InvariantEquals(value));
		}

		public static IGrammarEntry GetVariableEntry(this IGrammar grammar)
		{
			return grammar.Entries.First(e => e.Value.InvariantEquals(Symbol.Var));
		}

		public static IGrammarEntry GetConstantEntry(this IGrammar grammar)
		{
			return grammar.Entries.First(e => e.Value.InvariantEquals(Symbol.Const));
		}
		
		public static IGrammarEntry GetStringEntry(this IGrammar grammar)
		{
			return grammar.Entries.First(e => e.Value.InvariantEquals(Symbol.String));
		}
	}
}