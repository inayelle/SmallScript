using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.Grammars.BackusNaur.Grammar
{
	public class Rule : IRule
	{
		public NonTerminal        Root         { get; }
		public ISet<IAlternative> Alternatives { get; }

		public Rule(NonTerminal root, ISet<IAlternative> alternatives)
		{
			Root         = root ?? throw new ArgumentNullException(nameof(root));
			Alternatives = alternatives ?? throw new ArgumentNullException(nameof(alternatives));
		}

		public bool Equals(IRule other)
		{
			return Root.Equals(other.Root) && Alternatives.SequenceEqual(other.Alternatives);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Root, Alternatives);
		}

		public override string ToString()
		{
			return $"{Root} ::= {String.Join(" | ", Alternatives)}";
		}
	}
}