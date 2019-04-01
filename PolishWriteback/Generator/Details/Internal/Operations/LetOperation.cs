using System.Collections.Generic;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Generator.Base;

namespace SmallScript.PolishWriteback.Generator.Details.Internal.Operations
{
	internal class LetOperation : OperationBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal("let");
		
		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			var result = ProcessDijkstraUntilEntry(iterator, new NonTerminal("<EOL>"));
			
			iterator.MoveNext();

			return result;
		}
	}
}