using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Extensions;
using SmallScript.LexicalParsers.RegexParser.Parser.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Extensions;

[assembly: InternalsVisibleTo("SmallScript.LexicalParsers.RegexParser.Tests")]

namespace SmallScript.LexicalParsers.RegexParser.Parser.Details.Internal
{
	internal class TokenFactory : ITokenFactory
	{
		private readonly IDictionary<string, ConstantToken> _constantsCache;
		private readonly IGrammar                           _grammar;
		private readonly IIdentitySource                    _identitySource;
		private readonly IDictionary<string, VariableToken> _variablesCache;

		public TokenFactory(IGrammar grammar, IIdentitySource identitySource)
		{
			_grammar        = Require.NotNull(grammar, nameof(grammar));
			_identitySource = Require.NotNull(identitySource, nameof(identitySource));

			_constantsCache = new Dictionary<string, ConstantToken>();
			_variablesCache = new Dictionary<string, VariableToken>();
		}

		public IToken Create(string value, FilePosition position)
		{
			Require.NotNull(value, nameof(value));
			Require.NotNull(position, nameof(position));

			if (IsDelimiter(value, position, out var delimiter)) 
				return delimiter;
			
			if (IsKeyword(value, position, out var keyword)) 
				return keyword;
			
			if (IsConstant(value, position, out var constant)) 
				return constant;

			if (IsVariable(value, position, out var variable)) 
				return variable;

			return new InvalidToken(value, position);
		}

		private bool IsVariable(string value, FilePosition position, out VariableToken variableToken)
		{
			if (!Regex.IsMatch(value, @"^[A-z_]+$"))
			{
				variableToken = null;
				return false;
			}

			if (_variablesCache.ContainsKey(value))
			{
				variableToken = _variablesCache[value].CloneWithPosition(position);
			}
			else
			{
				var grammarEntry = _grammar.GetVariableEntry();
				var id           = _identitySource.NextVariableId;

				variableToken = _variablesCache[value] = new VariableToken(id, value, position, grammarEntry);
			}

			return true;
		}

		private bool IsConstant(string value, FilePosition position, out ConstantToken constantToken)
		{
			if (!value.All(char.IsDigit))
			{
				constantToken = null;
				return false;
			}

			if (_constantsCache.ContainsKey(value))
			{
				constantToken = _constantsCache[value].CloneWithPosition(position);
			}
			else
			{
				var grammarEntry = _grammar.GetConstantEntry();
				var id           = _identitySource.NextConstantId;

				_constantsCache[value] = new ConstantToken(id, value, position, grammarEntry);
			}

			constantToken = _constantsCache[value];
			return true;
		}

		private bool IsKeyword(string value, FilePosition position, out KeywordToken keywordToken)
		{
			var grammarEntry = _grammar.GetEntryByValue(value);

			if (grammarEntry == null || !value.All(char.IsLetter))
			{
				keywordToken = null;
				return false;
			}

			keywordToken = new KeywordToken(value, position, grammarEntry);
			return true;
		}

		private bool IsDelimiter(string value, FilePosition position, out DelimiterToken delimiterToken)
		{
			if (value.InvariantEquals("\n")) 
				value = "<EOL>";

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