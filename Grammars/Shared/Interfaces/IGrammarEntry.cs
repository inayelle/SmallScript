using System;

namespace SmallScript.Grammars.Shared.Interfaces
{
	public interface IGrammarEntry : IEquatable<IGrammarEntry>
	{
		string Value { get; }
	}
}