using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;

namespace SmallScript.Grammars.BackusNaur.Grammar.Details
{
	public class Rule : IRule
	{
		public Rule(NonTerminal root, ISet<IAlternative> alternatives)
		{
			Root         = Require.NotNull(root, nameof(root));
			Alternatives = Require.NotNull(alternatives, nameof(alternatives));
		}

		public NonTerminal        Root         { get; }
		public ISet<IAlternative> Alternatives { get; }

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

			return Equals(obj as IRule);
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
			return $"{Root} ::= {string.Join(" | ", Alternatives)}";
		}
	}
}