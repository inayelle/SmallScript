using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class PowerOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Power);

		public override void Execute(RuntimeData runtimeData)
		{
			var first = PopIntValue(runtimeData);
			var last  = PopIntValue(runtimeData);

			var result = (int) Math.Pow(last, first);
			
			PushValue(runtimeData, result);
		}
	}
}