using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.PolishWriteback.Executor.Internals.Tokens
{
	internal class BoolValueToken : IToken
	{
		public bool BoolValue { get; }

		public string Value => BoolValue.ToString();
		public FilePosition Position => null;

		public IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.BoolValue);

		public BoolValueToken(bool value)
		{
			BoolValue = value;
		}
		
		public BoolValueToken(int value)
		{
			BoolValue = value > 0;
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
			if (!(other is BoolValueToken b))
			{
				return false;
			}

			return BoolValue == b.BoolValue;
		}
	}
}