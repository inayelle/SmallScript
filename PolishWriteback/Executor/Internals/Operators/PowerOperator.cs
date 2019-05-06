using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class PowerOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Power);

		protected override void ExecuteImpl(Runtime runtime)
		{
			var first = runtime.PopInt();
			var last  = runtime.PopInt();

			var result = (int) Math.Pow(last, first);
			
			runtime.Push(result);
		}
	}
}