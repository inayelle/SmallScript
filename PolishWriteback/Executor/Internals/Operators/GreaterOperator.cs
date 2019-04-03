using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class GreaterOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Greater);
		
		public override void Execute(RuntimeData runtime)
		{
			var last  = runtime.PopInt();
			var first = runtime.PopInt();

			bool result = first > last;
			
			runtime.Push(result);
		}
	}
}