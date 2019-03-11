using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.BackusNaur.Grammar.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Errors;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Exceptions;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Enums;
using SmallScript.SyntaxParsers.PrecedenceParser.Parser.Extensions;
using SmallScript.SyntaxParsers.Shared.Details;
using SmallScript.SyntaxParsers.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser
{
	public class PrecedenceParser : ISyntaxParser
	{
		private readonly Generator.Details.Generator _generator;

		private IGrammar _grammar;

		public IGrammar Grammar
		{
			get => _grammar;
			set => _grammar = Require.NotNull(value);
		}

		public PrecedenceParser(IGrammar grammar)
		{
			Grammar    = grammar;
			_generator = new Generator.Details.Generator();
		}

		public SyntaxParserResult Parse(LexicalParseResult result)
		{
			try
			{
				if (!Parse(result.Tokens))
				{
					throw new Exception("HZ");
				}
			}
			catch (SmallScriptException exception)
			{
				return new SyntaxParserResult(new ParseError(exception.Message, new FilePosition(1, 1)));
			}

			return new SyntaxParserResult();
		}

		private bool Parse(IEnumerable<IToken> tokens)
		{
			var pairs = _generator.Generate(Grammar).Pairs;
			var stack = new Stack<IGrammarEntry>();

			stack.Push(tokens.First().GrammarEntry);

			foreach (var currentEntry in tokens.Skip(1).Select(t => t.GrammarEntry))
			{
				var left  = stack.Peek();
				var right = currentEntry;

				if (pairs[left, right].HasAnyRelation(RelationType.Less, RelationType.Equal))
				{
					stack.Push(right);
				}
				else
				{
					var sequence = new List<IGrammarEntry> { left };

					IGrammarEntry l;

					while (true)
					{
						l = stack.Pop();

						if (pairs[l, left].HasRelation(RelationType.Less))
						{
							break;
						}
						else
						{
							sequence.Add(l);
							left = l;
						}
					}

					sequence.Reverse();
					var alternative  = new Alternative(sequence);
					var grammarEntry = Grammar.With(alternative);
					stack.Push(grammarEntry);
					stack.Push(right);
				}
			}

			return stack.First().Equals(Grammar.Rules.First().Root);
		}
	}
}