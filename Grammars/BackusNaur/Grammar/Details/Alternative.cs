using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.Grammars.BackusNaur.Grammar.Details
{
	public class Alternative : IAlternative
	{
		public Alternative(IEnumerable<IGrammarEntry> entries)
		{
			Entries = entries ?? throw new ArgumentNullException(nameof(entries));
		}

		public IEnumerable<IGrammarEntry> Entries { get; }

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

			return Equals(obj as IAlternative);
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
			return string.Join(' ', Entries);
		}
	}
}