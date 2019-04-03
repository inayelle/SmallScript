using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class MinusOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Minus);

		public override void Execute(RuntimeData runtime)
		{
			var first = runtime.PopInt();
			var last  = runtime.PopInt();

			var result = first - last;
			
			runtime.Push(result);
		}
	}
}