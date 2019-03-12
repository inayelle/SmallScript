using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser.Details
{
	public class BoundEntry : IGrammarEntry
	{
		public string Value { get; }

		public BoundEntry(string value)
		{
			Value = Require.NotNull(value, nameof(value));
		}
		
		public override bool Equals(object obj)
		{
			return Equals(obj as IGrammarEntry);
		}

		public override int GetHashCode()
		{
			return Value.InvariantHashCode();
		}

		public bool Equals(IGrammarEntry other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}

			if (ReferenceEquals(other, this))
			{
				return true;
			}

			return other is BoundEntry && Value.InvariantEquals(other.Value);
		}

		public override string ToString()
		{
			return Value;
		}
	}
}