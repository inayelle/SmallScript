using System;
using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.Calculator.Extensions
{
	internal static class TokenExtensions
	{
		public static int GetPriority(this IToken token)
		{
			switch (token.Value)
			{
				case "(":
				case ")":
					return 0;
				case "+":
				case "-":
					return 1;
				case "*":
				case "/":
					return 2;
				case "**":
					return 3;
				default:
					throw new NotImplementedException();
			}
		}
	}
}