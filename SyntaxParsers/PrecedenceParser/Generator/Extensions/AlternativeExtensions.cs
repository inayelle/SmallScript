using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions
{
	internal static class AlternativeExtensions
	{
		public static IEnumerable<Pair> GetPairs(this IAlternative alternative)
		{
			var entries = alternative.Entries.ToArray();

			for (var i = 0; i < entries.Length - 1; ++i)
			{
				yield return new Pair(entries[i], entries[i + 1]);
			}
		}
	}
}