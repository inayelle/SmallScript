using System;
using System.Collections.Generic;

namespace SmallScript.Grammars.Shared.Interfaces
{
	public interface IAlternative : IEquatable<IAlternative>
	{
		IEnumerable<IGrammarEntry> Entries { get; }
	}
}