using System;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.Calculator.Extensions
{
	internal static class TokenExtensions
	{
		public static int GetPriority(this IToken token)
		{
			switch (token.Value)
			{
				case Symbol.OpenParenthesis:
				case Symbol.CloseParenthesis:
					return 0;
				case Symbol.Plus:
				case Symbol.Minus:
					return 1;
				case Symbol.Multiplication:
				case Symbol.Division:
					return 2;
				case Symbol.Power:
					return 3;
				default:
					throw new NotImplementedException();
			}
		}
	}
}