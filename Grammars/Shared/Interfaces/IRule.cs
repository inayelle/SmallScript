using System;
using System.Collections.Generic;
using SmallScript.Grammars.Shared.Details;

namespace SmallScript.Grammars.Shared.Interfaces
{
	public interface IRule : IEquatable<IRule>
	{
		NonTerminal        Root         { get; }
		ISet<IAlternative> Alternatives { get; }
	}
}