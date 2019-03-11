using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.Shared.Interfaces
{
	public interface IToken : IEquatable<IToken>
	{
		string Value { get; }

		FilePosition  Position     { get; }
		IGrammarEntry GrammarEntry { get; }
	}
}