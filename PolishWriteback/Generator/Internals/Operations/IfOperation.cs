using System.Collections.Generic;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Generator.Base;
using SmallScript.PolishWriteback.Generator.Details;

namespace SmallScript.PolishWriteback.Generator.Internals.Operations
{
	internal class IfOperation : OperationBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.OpenCondition);

		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			iterator.MoveNext(); //skip if

			return ProcessDijkstraUntilEntry(iterator, new Terminal(Symbol.Then));
		}
	}
}