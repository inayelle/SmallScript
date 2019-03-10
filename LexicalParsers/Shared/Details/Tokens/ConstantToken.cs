using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.Shared.Details.Tokens
{
	public class ConstantToken : TokenBase
	{
		private const string TokenCode = "T_CONSTANT";

		public int Id { get; }

		public ConstantToken(int id, string value, FilePosition position, IGrammarEntry grammarEntry) :
				base(TokenCode, value, position, grammarEntry)
		{
			Id = id;
		}

		public override bool Equals(IToken other)
		{
			return other is ConstantToken c && c.Id == Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(TokenCode, Id);
		}
	}
}