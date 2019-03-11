using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details
{
	public class SequenceRelations
	{
		private readonly List<IGrammarEntry> _first;
		private readonly List<IGrammarEntry> _last;
		private readonly List<IGrammarEntry> _firstPlus;
		private readonly List<IGrammarEntry> _lastPlus;

		public ICollection<IGrammarEntry> First     => _first.ToHashSet();
		public ICollection<IGrammarEntry> Last      => _last.ToHashSet();
		public ICollection<IGrammarEntry> FirstPlus => _firstPlus.ToHashSet();
		public ICollection<IGrammarEntry> LastPlus  => _lastPlus.ToHashSet();

		public SequenceRelations()
		{
			_first     = new List<IGrammarEntry>();
			_last      = new List<IGrammarEntry>();
			_firstPlus = new List<IGrammarEntry>();
			_lastPlus  = new List<IGrammarEntry>();
		}

		public void AddFirst(params IGrammarEntry[] args)
		{
			_first.AddRange(args);
		}

		public void AddLast(params IGrammarEntry[] args)
		{
			_last.AddRange(args);
		}

		public void AddFirstPlus(params IGrammarEntry[] args)
		{
			_firstPlus.AddRange(args);
		}

		public void AddLastPlus(params IGrammarEntry[] args)
		{
			_lastPlus.AddRange(args);
		}
	}
}