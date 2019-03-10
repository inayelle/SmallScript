using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SmallScript.Grammars.BackusNaur.Grammar;
using SmallScript.Grammars.BackusNaur.Parser.Exceptions;
using SmallScript.Grammars.BackusNaur.Parser.Interfaces;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Exceptions;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared;

namespace SmallScript.Grammars.BackusNaur.Parser.Details
{
	public class BackusNaurGrammarParser : IGrammarParser<BackusNaurGrammar>
	{
		private IEntryFactory _factory;

		public IEntryFactory EntryFactory
		{
			get => _factory;
			set => _factory = value ?? throw new ArgumentNullException(nameof(value));
		}

		public BackusNaurGrammarParser() : this(new CachingEntryFactory())
		{
		}

		public BackusNaurGrammarParser(IEntryFactory entryFactory)
		{
			EntryFactory = entryFactory ?? throw new ArgumentNullException(nameof(entryFactory));
		}

		public BackusNaurGrammar Parse(Stream stream)
		{
			using (var reader = new StreamReader(stream, Encoding.UTF8))
			{
				var input = reader.ReadToEnd();
				return Parse(input);
			}
		}

		public BackusNaurGrammar Parse(string input)
		{
			var lines    = SplitLines(input);
			var position = new Position();
			var rules    = new HashSet<IRule>();
	
			try
			{
				foreach (var entry in lines)
				{
					if (!IsCommentedOrEmptyLine(entry))
					{
						rules.Add(ParseRule(entry));
					}

					position.MoveNextLine();
				}
			}
			catch (GrammarParseException)
			{
				throw;
			}
			catch (Exception exception)
			{
				throw new GrammarParseException(position, exception);
			}

			return new BackusNaurGrammar(rules);
		}

		private static IList<string> SplitLines(string input)
		{
			return Regex.Split(input, "\n").Select(l => l.Trim()).ToList();
		}

		private IRule ParseRule(string rule)
		{
			var parts = rule.Split("::=", StringSplitOptions.RemoveEmptyEntries);

			if (parts.Length != 2)
			{
				throw new InvalidGrammarSyntaxException("Delimiter wrong usage");
			}

			var root         = _factory.CreateEntry(parts[0].Trim()) as NonTerminal;
			var alternatives = ParseAlternatives(parts[1].Trim());

			if (root == null)
			{
				throw new InvalidGrammarSyntaxException("Rule's root element must be declared as non-terminal");
			}

			return new Rule(root, alternatives);
		}

		private ISet<IAlternative> ParseAlternatives(string alternatives)
		{
			var parts = alternatives.Split("|", StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim());

			return parts.Select(ParseAlternative).ToHashSet();
		}

		private IAlternative ParseAlternative(string value)
		{
			var parts = Regex.Split(value, @"[\t ]").Select(p => p.Trim());

			var entries = parts.Select(_factory.CreateEntry);

			return new Alternative(entries);
		}

		private static bool IsCommentedOrEmptyLine(string value)
		{
			return String.IsNullOrWhiteSpace(value) || value.StartsWith("#");
		}
	}
}