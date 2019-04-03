using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.PolishWriteback.Generator.Extensions
{
	public static class TokenExtensions
	{
		public static int GetPriority(this IToken token)
		{
			switch (token.Value)
			{
				case Symbol.OperationDelimiter:
					return 75;
				case Symbol.Declare:
				case Symbol.Let:
				case Symbol.StandartInput:
				case Symbol.StandartOutput:
					return 100;
				case Symbol.Assign:
				case Symbol.StreamReading:
				case Symbol.StreamWriting:
					return 200;
				case Symbol.Greater:
				case Symbol.GreaterEqual:
				case Symbol.Less:
				case Symbol.LessEqual:
				case Symbol.Equal:
				case Symbol.NotEqual:
					return 275;
				case Symbol.OpenParenthesis:
				case Symbol.CloseParenthesis:
					return 300;
				case Symbol.Plus:
				case Symbol.Minus:
					return 400;
				case Symbol.Multiplication:
				case Symbol.Division:
					return 401;
				case Symbol.Power:
					return 402;
				default:
					return -1;
			}
		}
	}
}