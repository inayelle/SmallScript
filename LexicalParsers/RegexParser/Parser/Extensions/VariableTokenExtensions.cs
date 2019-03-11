using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.RegexParser.Parser.Extensions
{
	internal static class VariableTokenExtensions
	{
		public static VariableToken CloneWithPosition(this VariableToken token, FilePosition position)
		{
			return new VariableToken(token.Id, token.Value, position, token.GrammarEntry);
		}
	}
}