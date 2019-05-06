using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class PlusOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Plus);

		protected override void ExecuteImpl(Runtime runtime)
		{
			var first = runtime.PopInt();
			var last  = runtime.PopInt();

			var result = first + last;
			
			runtime.Push(result);
		}
	}
}