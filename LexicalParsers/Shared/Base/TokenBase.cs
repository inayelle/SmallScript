using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Extensions;

namespace SmallScript.LexicalParsers.Shared.Base
{
	public abstract class TokenBase : IToken
	{
		public string        Value        { get; }
		public FilePosition  Position     { get; }
		public IGrammarEntry GrammarEntry { get; }

		protected TokenBase(string value, FilePosition position, IGrammarEntry grammarEntry)
		{
			Value        = Require.NotNull(value, nameof(value));
			GrammarEntry = Require.NotNull(grammarEntry, nameof(grammarEntry));
			Position     = Require.NotNull(position, nameof(position));
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
			       other.Value.InvariantEquals(Value);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GrammarEntry, Value);
		}

		public override string ToString()
		{
			return $"{Value} : {Position}";
		}
	}
}