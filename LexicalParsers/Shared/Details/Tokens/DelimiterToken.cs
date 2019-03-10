using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.Shared.Details.Tokens
{
	public class DelimiterToken : TokenBase
	{
		public DelimiterToken(string code, string value, FilePosition position, IGrammarEntry grammarEntry)
				: base(code, value, position, grammarEntry)
		{
		}
	}
}