using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;

namespace SmallScript.Grammars.BackusNaur.Grammar.Details
{
	public class Alternative : IAlternative
	{
		public Alternative(IEnumerable<IGrammarEntry> entries)
		{
			Entries = Require.NotNull(entries, nameof(entries));
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
			var hash = new HashCode();

			foreach (var entry in Entries)
			{
				hash.Add(entry);
			}

			return hash.ToHashCode();
		}

		public override string ToString()
		{
			return string.Join(' ', Entries);
		}
	}
}