using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Exceptions;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class DivisionOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Division);

		protected override void ExecuteImpl(RuntimeData runtime)
		{
			var first = runtime.PopInt();
			var last  = runtime.PopInt();

			if (first == 0)
			{
				throw new DivisionByZeroException(runtime.Iterator.Current);
			}
			
			var result = last / first;

			runtime.Push(result);
		}
	}
}