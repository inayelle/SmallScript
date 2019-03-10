using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.Shared.Details.Tokens
{
	public class KeywordToken : TokenBase
	{
		public KeywordToken(string code, string value, FilePosition position, IGrammarEntry grammarEntry) 
				: base(code, value, position, grammarEntry)
		{
		}
	}
}