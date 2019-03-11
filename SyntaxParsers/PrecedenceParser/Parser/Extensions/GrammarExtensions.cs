using System.Linq;
using SmallScript.Grammars.BackusNaur.Grammar.Details;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser.Extensions
{
	internal static class GrammarExtensions
	{
		public static IGrammarEntry With(this IGrammar grammar, Alternative alternative)
		{
			return grammar.Rules.First(r => r.Alternatives.Contains(alternative)).Root;
		}
	}
}