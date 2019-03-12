using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions
{
	internal static class RuleSetExtensions
	{
		public static IEnumerable<IAlternative> GetAllAlternatives(this IGrammar grammar)
		{
			return grammar.Rules.SelectMany(r => r.Alternatives).ToList();
		}
	}
}