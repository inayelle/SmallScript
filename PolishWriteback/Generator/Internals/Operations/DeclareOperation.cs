using System.Collections.Generic;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Generator.Base;
using SmallScript.PolishWriteback.Generator.Details;

namespace SmallScript.PolishWriteback.Generator.Internals.Operations
{
	internal class DeclareOperation : OperationBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal("decl");
		
		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			var result = ProcessDijkstraUntilEntry(iterator, new NonTerminal("<EOL>"));
			
			iterator.MoveNext();

			return result;
		}
	}
}