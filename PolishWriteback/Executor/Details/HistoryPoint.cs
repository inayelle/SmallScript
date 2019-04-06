using System;
using System.Collections.Generic;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Details
{
	public sealed class HistoryPoint
	{
		public IGrammarEntry OperatorEntry { get; }

		public Stack<IToken> Stack { get; }

		public HistoryPoint(IGrammarEntry operatorEntry, Stack<IToken> stack)
		{
			OperatorEntry = operatorEntry;

			Stack = stack.Clone();
		}

		public override string ToString()
		{
			return $"Operator: [{OperatorEntry.Value}] | Stack: [{String.Join(", ", Stack)}]";
		}
	}
}