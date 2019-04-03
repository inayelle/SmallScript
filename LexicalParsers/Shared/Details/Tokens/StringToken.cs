using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Base;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.Shared.Details.Tokens
{
	public class StringToken : TokenBase
	{
		public StringToken(string value, FilePosition position, IGrammarEntry grammarEntry)
				: base(Prepare(value), position, grammarEntry)
		{
		}
		
		public override bool Equals(IToken other)
		{
			return other is StringToken s && s.Value == Value;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GrammarEntry, Value);
		}
		
		public override string ToString()
		{
			return $"[{nameof(StringToken)}] {Value} : {Position}";
		}

		private static string Prepare(string value)
		{
			if (value == @"\n")
			{
				value = "\n";
			}

			return value;
		}
	}
}