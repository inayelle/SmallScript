using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Details.Internal;
using SmallScript.LexicalParsers.RegexParser.Parser.Interfaces;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Errors;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Exceptions;

namespace SmallScript.LexicalParsers.RegexParser.Parser.Details
{
	public class RegexParser : ILexicalParser
	{
		private ITokenFactory       _factory;
		private ISourceCodeSplitter _splitter;

		public event Action<IList<IToken>> OnSuccessfulParse;
		public event Action<IList<IToken>> OnFailedParse;

		public RegexParser(IGrammar grammar) : this(new SourceCodeSplitter(), grammar)
		{
		}

		public RegexParser(ITokenFactory tokenFactory) : this(new SourceCodeSplitter(), tokenFactory)
		{
		}

		public RegexParser(ISourceCodeSplitter splitter, IGrammar grammar)
				: this(splitter, new TokenFactory(grammar, new IdentitySource()))
		{
		}

		public RegexParser(ISourceCodeSplitter splitter, ITokenFactory tokenFactory)
		{
			Splitter     = splitter;
			TokenFactory = tokenFactory;
		}

		public ITokenFactory TokenFactory
		{
			get => _factory;
			set => _factory = Require.NotNull(value);
		}

		public ISourceCodeSplitter Splitter
		{
			get => _splitter;
			set => _splitter = Require.NotNull(value);
		}

		public LexicalParseResult Parse(string input)
		{
			Require.NotNull(input, nameof(input));

			var lines      = _splitter.SplitByLines(input.Trim());
			var tokens     = new List<IToken>();
			var navigation = new FileNavigation();

			try
			{
				foreach (var line in lines)
				{
					tokens.AddRange(ParseLine(line, navigation));

					navigation.MoveLine();
				}
			}
			catch (SmallScriptException exception)
			{
				OnFailedParse?.Invoke(tokens);
				return new LexicalParseResult(new ParseError(exception.Message, navigation.CurrentPosition));
			}
			
			OnSuccessfulParse?.Invoke(tokens);

			return new LexicalParseResult(tokens);
		}

		public LexicalParseResult Parse(Stream stream)
		{
			Require.NotNull(stream, nameof(stream));

			using (var reader = new StreamReader(stream, Encoding.UTF8))
			{
				return Parse(reader.ReadToEnd());
			}
		}

		private IEnumerable<IToken> ParseLine(string line, FileNavigation navigation)
		{
			return _splitter.SplitByTokens(line).Select(t => _factory.Create(t, navigation.CurrentPosition));
		}
	}
}