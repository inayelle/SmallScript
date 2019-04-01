using System.Collections.Generic;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.WritebackGenerator.Generator.Details;

namespace SmallScript.WritebackGenerator.Generator.Interfaces
{
	public interface IOperation
	{
		IDictionary<IGrammarEntry, IOperation> Operations { get; set; }

		IGrammarEntry GrammarEntry { get; }

		IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack);
	}
}