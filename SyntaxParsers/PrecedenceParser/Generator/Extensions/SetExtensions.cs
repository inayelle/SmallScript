using System.Collections.Generic;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions
{
	internal static class SetExtensions
	{
		public static void AddRange<T>(this ISet<T> set, IEnumerable<T> range)
		{
			foreach (var item in range)
			{
				set.Add(item);
			}
		}
	}
}