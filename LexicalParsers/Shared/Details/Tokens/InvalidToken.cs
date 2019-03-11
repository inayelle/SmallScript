using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Extensions;

namespace SmallScript.LexicalParsers.Shared.Details.Tokens
{
	public class InvalidToken : IToken
	{
		public InvalidToken(string value, FilePosition position)
		{
			Value    = value;
			Position = position;
		}

		public IGrammarEntry GrammarEntry => null;

		public string       Value    { get; }
		public FilePosition Position { get; }

		public bool Equals(IToken other)
		{
			return other is InvalidToken && other.Value.InvariantEquals(Value);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(nameof(InvalidToken), Value);
		}

		public override string ToString()
		{
			return Value;
		}
	}
}