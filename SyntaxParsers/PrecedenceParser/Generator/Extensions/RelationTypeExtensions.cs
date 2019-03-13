using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Enums;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions
{
	public static class RelationTypeExtensions
	{
		public static string AsString(this RelationType relationType)
		{
			switch (relationType)
			{
				case RelationType.Greater:
				{
					return ">";
				}
				case RelationType.Less:
				{
					return "<";
				}
				case RelationType.Equal:
				{
					return "=";
				}
				default:
				{
					throw new NotImplementedException();
				}
			}
		}

		public static string AsString(this IEnumerable<RelationType> relations)
		{
			return String.Join(' ', relations.Select(r => r.AsString()));
		}
	}
}