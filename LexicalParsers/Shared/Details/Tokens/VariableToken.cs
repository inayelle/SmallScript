using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.Shared.Details.Tokens
{
	public class VariableToken : TokenBase
	{
		public VariableToken(int id, string value, FilePosition position, IGrammarEntry grammarEntry)
				: base(value, position, grammarEntry)
		{
			Id = id;
		}

		public int Id { get; }
	}
}