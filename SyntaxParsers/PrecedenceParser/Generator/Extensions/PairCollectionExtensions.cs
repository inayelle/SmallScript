using System.Collections.Generic;
using System.Linq;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions
{
	internal static class PairCollectionExtensions
	{
		public static IEnumerable<Pair> WhereBothAreNonTerminals(this PairCollection collection)
		{
			return collection.Where(x => x.Left is DetailedNonTerminal)
			                 .Where(x => x.Right is DetailedNonTerminal);
		}
		
		public static IEnumerable<Pair> WhereLeftIsNonTerminal(this PairCollection collection)
		{
			return collection.Where(x => x.Left is DetailedNonTerminal);
		}
		
		public static IEnumerable<Pair> WhereRightIsNonTerminal(this PairCollection collection)
		{
			return collection.Where(x => x.Right is DetailedNonTerminal);
		}
	}
}