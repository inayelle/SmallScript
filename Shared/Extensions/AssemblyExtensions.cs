using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmallScript.Shared.Extensions
{
	public static class AssemblyExtensions
	{
		public static IEnumerable<Type> GetTypes(this Assembly assembly, string @namespace)
		{
			return assembly.GetTypes()
			               .Where(t => t.Namespace != null)
			               .Where(t => t.Namespace.Equals(@namespace, StringComparison.Ordinal))
			               .ToArray();
		}
	}
}