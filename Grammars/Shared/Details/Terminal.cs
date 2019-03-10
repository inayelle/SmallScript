using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Extensions;

namespace SmallScript.Grammars.Shared.Details
{
	public class Terminal : IGrammarEntry
	{
		public string Value { get; }

		public Terminal(string value)
		{
			Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public bool Equals(IGrammarEntry other)
		{
			return other is Terminal && Value.InvariantEquals(other.Value);
		}

		public override int GetHashCode()
		{
			return Value.InvariantHashCode();
		}

		public override string ToString()
		{
			return Value;
		}
	}
}