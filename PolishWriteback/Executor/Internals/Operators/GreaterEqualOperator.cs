using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class GreaterEqualOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.GreaterEqual);
		
		protected override void ExecuteImpl(RuntimeData runtime)
		{
			var last  = runtime.PopInt();
			var first = runtime.PopInt();

			bool result = first >= last;
			
			runtime.Push(result);
		}
	}
}