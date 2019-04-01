using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.WritebackGenerator.Generator.Extensions
{
	public static class TokenExtensions
	{
		public static int GetPriority(this IToken token)
		{
			switch (token.Value)
			{
				case "<EOL>":
					return 75;
				case "decl":
				case "let":
				case "stdin":
				case "stdout":
					return 100;
				case "=":
				case ">>":
				case "<<":
					return 200;
				case ">":
				case ">=":
				case "<":
				case "<=":
				case "==":
				case "!=":
					return 275;
				case "(":
				case ")":
					return 300;
				case "+":
				case "-":
					return 400;
				case "*":
				case "/":
					return 401;
				case "**":
					return 402;
				default:
					return -1;
			}
		}
	}
}