using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Extensions;

namespace SmallScript.Grammars.Shared.Details
{
	public class NonTerminal : IGrammarEntry
	{
		public NonTerminal(string value)
		{
			Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public string Value { get; }

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

			return Equals(obj as IGrammarEntry);
		}
		
		public bool Equals(IGrammarEntry other)
		{
			return other is NonTerminal && Value.InvariantEquals(other.Value);
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