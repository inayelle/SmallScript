using System.Collections.Generic;

namespace SmallScript.PolishWriteback.Executor.Extensions
{
	internal static class StackExtensions
	{
		public static Stack<T> Clone<T>(this Stack<T> stack)
		{
			return new Stack<T>(new Stack<T>(stack));
		}
	}
}