using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details
{
	public class SequenceRelations
	{
		private readonly HashSet<IGrammarEntry> _first;
		private readonly HashSet<IGrammarEntry> _last;
		private readonly HashSet<IGrammarEntry> _firstPlus;
		private readonly HashSet<IGrammarEntry> _lastPlus;

		public IEnumerable<IGrammarEntry> First     => _first;
		public IEnumerable<IGrammarEntry> Last      => _last;
		public IEnumerable<IGrammarEntry> FirstPlus => _firstPlus;
		public IEnumerable<IGrammarEntry> LastPlus  => _lastPlus;

		public SequenceRelations()
		{
			_first     = new HashSet<IGrammarEntry>();
			_last      = new HashSet<IGrammarEntry>();
			_firstPlus = new HashSet<IGrammarEntry>();
			_lastPlus  = new HashSet<IGrammarEntry>();
		}

		public void AddFirst(IGrammarEntry entry)
		{
			_first.Add(entry);
		}
		
		public void AddFirst(IEnumerable<IGrammarEntry> args)
		{
			_first.AddRange(args);
		}

		public void AddLast(IGrammarEntry entry)
		{
			_last.Add(entry);
		}
		
		public void AddLast(IEnumerable<IGrammarEntry> args)
		{
			_last.AddRange(args);
		}

		public void AddFirstPlus(IGrammarEntry entry)
		{
			_firstPlus.Add(entry);
		}
		
		public void AddFirstPlus(IEnumerable<IGrammarEntry> entries)
		{
			_firstPlus.AddRange(entries);
		}

		public void AddLastPlus(IGrammarEntry entry)
		{
			_lastPlus.Add(entry);
		}
		
		public void AddLastPlus(IEnumerable<IGrammarEntry> entries)
		{
			_lastPlus.AddRange(entries);
		}
	}
}