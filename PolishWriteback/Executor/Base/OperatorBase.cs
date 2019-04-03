using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals;

namespace SmallScript.PolishWriteback.Executor.Base
{
	internal abstract class OperatorBase : IOperator
	{
		public abstract IGrammarEntry GrammarEntry { get; }

		public abstract void Execute(RuntimeData runtimeData);
	}
}