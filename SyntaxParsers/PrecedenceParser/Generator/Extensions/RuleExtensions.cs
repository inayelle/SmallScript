using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Resolvers;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions
{
	internal static class RuleExtensions
	{
		public static void Accept(this IRule rule, FirstLastResolver resolver)
		{
			resolver.Visit(rule);
		}
		
		public static void Accept(this IRule rule, FirstLastPlusResolver resolver)
		{
			resolver.Visit(rule);
		}
	}
}