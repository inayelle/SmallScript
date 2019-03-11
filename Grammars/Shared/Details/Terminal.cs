using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Extensions;

namespace SmallScript.Grammars.Shared.Details
{
	public class Terminal : IGrammarEntry
	{
		public Terminal(string value)
		{
			Value = Require.NotNull(value, nameof(value));
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
			return other is Terminal && Value.InvariantEquals(other.Value);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(nameof(Terminal), Value);
		}

		public override string ToString()
		{
			return Value;
		}
	}
}