using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.Shared.Details.Tokens
{
	public class VariableToken : TokenBase
	{
		private const string TokenCode = "T_VARIABLE";

		public int    Id   { get; }
		public string Name { get; }

		public VariableToken(int id, string name, string value, FilePosition position, IGrammarEntry grammarEntry)
				: base(TokenCode, value, position, grammarEntry)
		{
			Id   = id;
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}
	}
}