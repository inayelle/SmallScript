using System.Collections.Generic;

namespace SmallScript.Grammars.Shared.Interfaces
{
	public interface IGrammar
	{
		ISet<IGrammarEntry>      Entries { get; }
		ISet<IRule>              Rules   { get; }
	}
}