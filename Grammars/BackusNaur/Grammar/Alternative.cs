using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.Grammars.BackusNaur.Grammar
{
	public class Alternative : IAlternative
	{
		public IEnumerable<IGrammarEntry> Entries { get; }

		public Alternative(IEnumerable<IGrammarEntry> entries)
		{
			Entries = entries ?? throw new ArgumentNullException(nameof(entries));
		}

		public bool Equals(IAlternative other)
		{
			return Entries.SequenceEqual(other.Entries);
		}

		public override int GetHashCode()
		{
			return Entries.GetHashCode();
		}

		public override string ToString()
		{
			return String.Join(' ', Entries);
		}
	}
}