using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Extensions;

namespace SmallScript.LexicalParsers.Shared.Base
{
	public abstract class TokenBase : IToken
	{
		protected TokenBase(string value, FilePosition position, IGrammarEntry grammarEntry)
		{
			Value        = Require.NotNull(value, nameof(value));
			GrammarEntry = Require.NotNull(grammarEntry, nameof(grammarEntry));
			Position     = position;
		}

		public string        Value        { get; }
		public FilePosition  Position     { get; }
		public IGrammarEntry GrammarEntry { get; }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(obj, null))
			{
				return false;
			}

			if (ReferenceEquals(obj, this))
			{
				return true;
			}

			return Equals(obj as IToken);
		}

		public virtual bool Equals(IToken other)
		{
			return other.GetType() == GetType() &&
			       other.Value.InvariantEquals(Value);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GrammarEntry, Value);
		}

		public override string ToString()
		{
			return $"[{GetType().Name}] {Value} : {Position}";
		}
	}
}