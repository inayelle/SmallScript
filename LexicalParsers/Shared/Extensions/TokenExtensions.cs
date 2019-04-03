using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Extensions;

namespace SmallScript.LexicalParsers.Shared.Extensions
{
	public static class TokenExtensions
	{
		public static bool IsKeyword(this IToken token)
		{
			return token is KeywordToken;
		}

		public static bool IsKeyword(this IToken token, string expectedValue)
		{
			return token is KeywordToken && token.Value.InvariantEquals(expectedValue);
		}

		public static bool IsDelimiter(this IToken token)
		{
			return token is DelimiterToken;
		}

		public static bool IsDelimiter(this IToken token, string expectedValue)
		{
			return token is DelimiterToken && token.Value.InvariantEquals(expectedValue);
		}

		public static bool IsDelimiter(this IToken token, char expectedValue)
		{
			return IsDelimiter(token, expectedValue.ToString());
		}

		public static bool IsConstant(this IToken token)
		{
			return token is ConstantToken;
		}

		public static bool IsConstant(this IToken token, string expectedValue)
		{
			return token is ConstantToken c && c.Value.InvariantEquals(expectedValue);
		}

		public static bool IsVariable(this IToken token)
		{
			return token is VariableToken;
		}

		public static bool IsVariable(this IToken token, string expectedValue)
		{
			return token is VariableToken v && v.Value.InvariantEquals(expectedValue);
		}

		public static bool IsString(this IToken token)
		{
			return token is StringToken;
		}
	}
}