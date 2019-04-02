using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.PolishWriteback.Executor.Internals.Tokens
{
	internal class IntValueToken : IToken
	{
		public int IntValue { get; }

		public string       Value    => IntValue.ToString();
		public FilePosition Position => null;

		public IGrammarEntry GrammarEntry { get; } = new Terminal("@VALUE");

		public IntValueToken(int value)
		{
			IntValue = value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(obj, this))
			{
				return true;
			}

			return Equals(obj as IToken);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Value, GrammarEntry);
		}

		public bool Equals(IToken other)
		{
			if (!(other is IntValueToken v))
			{
				return false;
			}

			return v.IntValue == IntValue;
		}
	}
}