using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.LexicalParsers.Shared.Interfaces;
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
		
		public override bool Equals(IToken other)
		{
			return other is VariableToken v && v.Id == Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GrammarEntry, Id);
		}
		
		public override string ToString()
		{
			return $"[{nameof(VariableToken)}, {Id}] {Value} : {Position}";
		}
	}
}