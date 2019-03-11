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
	}
}