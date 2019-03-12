using SmallScript.Grammars.Shared.Details;
using SmallScript.Shared.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details
{
	public class DetailedNonTerminal : NonTerminal
	{
		public bool FirstPlusVisited { get; set; }
		public bool LastPlusVisited  { get; set; }

		public SequenceRelations SequenceRelations { get; }

		public DetailedNonTerminal(string value) : base(value)
		{
			SequenceRelations = new SequenceRelations();

			FirstPlusVisited = false;
			LastPlusVisited  = false;
		}

		public DetailedNonTerminal(NonTerminal nonTerminal) : this(nonTerminal.Value)
		{
		}

		public bool Equals(DetailedNonTerminal other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}

			if (ReferenceEquals(other, this))
			{
				return true;
			}

			return Value.InvariantEquals(other.Value);
		}
	}
}