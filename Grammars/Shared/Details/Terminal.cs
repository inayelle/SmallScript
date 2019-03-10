using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Extensions;

namespace SmallScript.Grammars.Shared.Details
{
	public class Terminal : IGrammarEntry
	{
		public string       Value { get; }

		public Terminal(string value)
		{
			Value = Require.NotNull(value, nameof(value));
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