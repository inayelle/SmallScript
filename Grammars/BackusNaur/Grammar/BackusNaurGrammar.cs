using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.Grammars.BackusNaur.Grammar
{
	public class BackusNaurGrammar : IGrammar
	{
		public ISet<IGrammarEntry> Entries { get; }
		public ISet<IRule>         Rules   { get; }

		public BackusNaurGrammar(ISet<IRule> rules)
		{
			Rules   = rules ?? throw new ArgumentNullException(nameof(rules));
			Entries = rules.SelectMany(r => r.Alternatives
			                                 .SelectMany(a => a.Entries)
			                                 .Append(r.Root))
			               .ToHashSet();
		}
	}
}