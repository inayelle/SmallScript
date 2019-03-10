using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.Shared.Details.Tokens
{
	public class ConstantToken : TokenBase
	{
		public int Id { get; }

		public ConstantToken(int id, string value, FilePosition position, IGrammarEntry grammarEntry) :
				base(value, position, grammarEntry)
		{
			Id = id;
		}

		public override bool Equals(IToken other)
		{
			return other is ConstantToken c && c.Id == Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GrammarEntry, Id);
		}
		
		public static ConstantToken CloneWithPosition(ConstantToken other, FilePosition position)
		{
			return new ConstantToken(other.Id, other.Value, position, other.GrammarEntry);
		}
	}
}