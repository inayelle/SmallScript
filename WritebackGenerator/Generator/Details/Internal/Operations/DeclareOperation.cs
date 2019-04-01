using System.Collections.Generic;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.WritebackGenerator.Generator.Base;
using SmallScript.WritebackGenerator.Generator.Interfaces;

namespace SmallScript.WritebackGenerator.Generator.Details.Internal.Operations
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