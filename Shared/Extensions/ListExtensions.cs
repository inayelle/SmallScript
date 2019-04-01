using System.Collections.Generic;

namespace SmallScript.Shared.Extensions
{
	public static class ListExtensions
	{
		public static void Add<T>(this List<T> list, params T[] args)
		{
			list.AddRange(args);
		}
	}
}