using System.Linq;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions
{
	public static class PairExtensions
	{
		public static bool IsFaulty(this Pair pair)
		{
			return pair.Relations.Count() > 1;
		}
	}
}