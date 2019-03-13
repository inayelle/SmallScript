using System.Linq;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Enums;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser.Extensions
{
	internal static class PairExtensions
	{
		public static bool HasAnyRelation(this Pair pair, params RelationType[] relations)
		{
			return relations.Any(pair.HasRelation);
		}
	}
}