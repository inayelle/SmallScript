using System;
using System.Linq;
using System.Runtime.CompilerServices;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Extensions;
using SmallScript.LexicalParsers.RegexParser.Parser.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Navigation;

[assembly: InternalsVisibleTo("SmallScript.LexicalParsers.RegexParser.Tests")]

namespace SmallScript.LexicalParsers.RegexParser.Parser.Details.Internal
{
	internal class TokenFactory : ITokenFactory
	{
		private readonly IGrammar        _grammar;
		private readonly IIdentitySource _identitySource;

		public TokenFactory(IGrammar grammar, IIdentitySource identitySource)
		{
			_grammar        = Require.NotNull(grammar, nameof(grammar));
			_identitySource = Require.NotNull(identitySource, nameof(identitySource));
		}

		public IToken Create(string value, FilePosition position)
		{
			Require.NotNull(value, nameof(value));
			Require.NotNull(position, nameof(position));

			if (IsVariable(value, position, out var variable))
			{
				return variable;
			}

			if (IsConstant(value, position, out var constant))
			{
				return constant;
			}

			if (IsKeyword(value, position, out var keyword))
			{
				return keyword;
			}

			if (IsDelimiter(value, position, out var delimiter))
			{
				return delimiter;
			}

			return new InvalidToken(value, position);
		}

		private bool IsVariable(string value, FilePosition position, out VariableToken variableToken)
		{
			if (!value.StartsWith('$') && value.Length > 1)
			{
				variableToken = null;
				return false;
			}

			var name         = value.Substring(1);
			var grammarEntry = _grammar.GetVariableEntry();
			var id           = _identitySource.NextVariableId;

			variableToken = new VariableToken(id, name, position, grammarEntry);
			return true;
		}

		private bool IsConstant(string value, FilePosition position, out ConstantToken constantToken)
		{
			if (!value.All(Char.IsDigit))
			{
				constantToken = null;
				return false;
			}

			var grammarEntry = _grammar.GetConstantEntry();
			var id           = _identitySource.NextConstantId;

			constantToken = new ConstantToken(id, value, position, grammarEntry);
			return true;
		}

		private bool IsKeyword(string value, FilePosition position, out KeywordToken keywordToken)
		{
			var grammarEntry = _grammar.GetEntryByValue(value);
			
			if (grammarEntry == null || !value.All(Char.IsLetter))
			{
				keywordToken = null;
				return false;
			}
		
			keywordToken = new KeywordToken(value, position, grammarEntry);
			return true;
		}
		
		private bool IsDelimiter(string value, FilePosition position, out DelimiterToken delimiterToken)
		{
			var grammarEntry = _grammar.GetEntryByValue(value);
			
			if (grammarEntry == null)
			{
				delimiterToken = null;
				return false;
			}
		
			delimiterToken = new DelimiterToken(value, position, grammarEntry);
			return true;
		}
	}
}