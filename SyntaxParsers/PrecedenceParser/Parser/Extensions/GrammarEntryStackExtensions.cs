using System.Collections.Generic;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser.Extensions
{
	internal static class GrammarEntryStackExtensions
	{
		public static IEnumerable<IGrammarEntry> PopUntilLess(this Stack<IGrammarEntry> stack)
		{
			while (true)
			{
				var left  = stack.Pop();
				var right = stack.Pop();
				
				
			}
		}
	}
}