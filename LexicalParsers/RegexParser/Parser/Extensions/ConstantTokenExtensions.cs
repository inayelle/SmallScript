using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.RegexParser.Parser.Extensions
{
	internal static class ConstantTokenExtensions
	{
		public static ConstantToken CloneWithPosition(this ConstantToken token, FilePosition position)
		{
			return new ConstantToken(token.Id, token.Value, position, token.GrammarEntry);
		}
	}
}