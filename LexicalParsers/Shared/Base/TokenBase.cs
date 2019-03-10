using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Extensions;

namespace SmallScript.LexicalParsers.Shared.Base
{
	public abstract class TokenBase : IToken
	{
		public string        Code         { get; }
		public string        Value        { get; }
		public FilePosition  Position     { get; }
		public IGrammarEntry GrammarEntry { get; }

		protected TokenBase(string code, string value, FilePosition position, IGrammarEntry grammarEntry)
		{
			Code         = code;
			Value        = value;
			Position     = position;
			GrammarEntry = grammarEntry;
		}

		public virtual bool Equals(IToken other)
		{
			if (other == null)
			{
				return false;
			}

			if (other == this)
			{
				return true;
			}

			return other.GetType() == GetType() &&
			       other.Value.InvariantEquals(Value) &&
			       other.Code.InvariantEquals(Code);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Code, Value);
		}

		public override string ToString()
		{
			return $"{Code} [{Value}] {Position}";
		}
	}
}