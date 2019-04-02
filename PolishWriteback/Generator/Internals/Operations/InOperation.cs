using System.Collections.Generic;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Generator.Base;
using SmallScript.PolishWriteback.Generator.Details;

namespace SmallScript.PolishWriteback.Generator.Internals.Operations
{
	internal class InOperation : OperationBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.StandartInput);
		
		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			var result = ProcessDijkstraUntilEntry(iterator, new NonTerminal(Symbol.OperationDelimiter));
			
			iterator.MoveNext();

			return result;
		}
	}
}