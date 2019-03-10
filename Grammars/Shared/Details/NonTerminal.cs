using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Extensions;

namespace SmallScript.Grammars.Shared.Details
{
	public class NonTerminal : IGrammarEntry
	{
		public string Value { get; }

		public NonTerminal(string value)
		{
			Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public bool Equals(IGrammarEntry other)
		{
			return other is NonTerminal && Value.InvariantEquals(other.Value);
		}

		public override string ToString()
		{
			return Value;
		}
	}
}