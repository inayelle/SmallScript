using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.Calculator.Tests.Details
{
	public class UnaryMinusDelimiter : DelimiterToken
	{
		public UnaryMinusDelimiter(FilePosition position, IGrammarEntry grammarEntry) : base("-", position, grammarEntry)
		{
		}
	}
}