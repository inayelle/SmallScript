using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Errors;

namespace SmallScript.LexicalParsers.Shared.Details
{
	public class LexicalParseResult
	{
		public IEnumerable<IToken>        Tokens    { get; }
		public IEnumerable<ConstantToken> Constants { get; }
		public IEnumerable<VariableToken> Variables { get; }

		public bool       Ok    { get; }
		public ParseError Error { get; }

		public LexicalParseResult(IEnumerable<IToken> tokens)
		{
			var enumerable = tokens as IToken[] ?? tokens.ToArray();

			Tokens    = enumerable;
			Constants = enumerable.OfType<ConstantToken>().Distinct();
			Variables = enumerable.OfType<VariableToken>().Distinct();

			Ok    = true;
			Error = null;
		}

		public LexicalParseResult(ParseError error)
		{
			Ok    = false;
			Error = error;

			Tokens    = Array.Empty<IToken>();
			Constants = Array.Empty<ConstantToken>();
			Variables = Array.Empty<VariableToken>();
		}
	}
}