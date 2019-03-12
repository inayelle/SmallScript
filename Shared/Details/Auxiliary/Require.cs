using System;
using System.Runtime.CompilerServices;

namespace SmallScript.Shared.Details.Auxiliary
{
	public static class Require
	{
		public static T NotNull<T>(T instance, [CallerMemberName] string name = null)
				where T : class
		{
			if (instance == null) throw new ArgumentNullException(name);

			return instance;
		}

		public static TTarget OfType<TTarget>(object instance, [CallerMemberName] string name = null)
				where TTarget : class
		{
			if (instance is TTarget t)
			{
				return t;
			}

			var actualType = instance?.GetType().Name ?? "null";
			throw new ArgumentException($"{name} expected to be {typeof(TTarget).Name}, got {actualType}");
		}
	}
}