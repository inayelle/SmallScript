using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Details;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals;

namespace SmallScript.PolishWriteback.Executor.Base
{
	internal abstract class OperatorBase : IOperator
	{
		public abstract IGrammarEntry GrammarEntry { get; }

		public event EventHandler<HistoryPoint> OnExecution;

		public void Execute(RuntimeData runtimeData)
		{
			ExecuteImpl(runtimeData);

			var args = new HistoryPoint(GrammarEntry, runtimeData.Stack);
			OnExecution?.Invoke(this, args);
		}

		protected abstract void ExecuteImpl(RuntimeData runtimeData);
	}
}