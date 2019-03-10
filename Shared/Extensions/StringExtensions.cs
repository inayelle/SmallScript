using System;

namespace SmallScript.Shared.Extensions
{
	public static class StringExtensions
	{
		public static int InvariantCompare(this string arg0, string arg1)
		{
			return StringComparer.InvariantCulture.Compare(arg0, arg1);
		}

		public static int InvariantHashCode(this string arg)
		{
			return StringComparer.InvariantCulture.GetHashCode(arg);
		}

		public static bool InvariantEquals(this string arg0, string arg1)
		{
			return StringComparer.InvariantCulture.Equals(arg0, arg1);
		}
	}
}