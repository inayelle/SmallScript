using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;

namespace SmallScript.Grammars.BackusNaur.Grammar.Details
{
	public class BackusNaurGrammar : IGrammar
	{
		public BackusNaurGrammar(ISet<IRule> rules)
		{
			Rules = Require.NotNull(rules, nameof(rules));

			Entries = rules.SelectMany(r => r.Alternatives
			                                 .SelectMany(a => a.Entries)
			                                 .Append(r.Root))
			               .ToHashSet();
		}

		public ISet<IGrammarEntry> Entries { get; }
		public ISet<IRule>         Rules   { get; }
	}
}