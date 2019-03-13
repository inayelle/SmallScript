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
		public IReadOnlyCollection<IToken>        Tokens        { get; }
		public IReadOnlyCollection<ConstantToken> Constants     { get; }
		public IReadOnlyCollection<VariableToken> Variables     { get; }
		public IReadOnlyCollection<InvalidToken>  InvalidTokens { get; }

		public bool       Ok    { get; }
		public ParseError Error { get; }
		
		public LexicalParseResult(IEnumerable<IToken> tokens)
		{
			var enumerable = tokens as IToken[] ?? tokens.ToArray();

			Tokens        = enumerable;
			Constants     = enumerable.OfType<ConstantToken>().Distinct().ToArray();
			Variables     = enumerable.OfType<VariableToken>().Distinct().ToArray();
			InvalidTokens = enumerable.OfType<InvalidToken>().ToList();

			var invalidTokens = enumerable.OfType<InvalidToken>().ToList();
			if (invalidTokens.Count > 0)
			{
				var firstInvalidToken = invalidTokens.First();

				var message  = $"Unexpected token {firstInvalidToken}";
				var position = firstInvalidToken.Position;

				Ok    = false;
				Error = new ParseError(message, position);
			}
			else
			{
				Ok    = true;
				Error = null;
			}
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