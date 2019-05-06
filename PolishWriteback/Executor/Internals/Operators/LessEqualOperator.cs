using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class LessEqualOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.LessEqual);
		
		protected override void ExecuteImpl(Runtime runtime)
		{
			var last  = runtime.PopInt();
			var first = runtime.PopInt();

			bool result = first <= last;
			
			runtime.Push(result);
		}
	}
}