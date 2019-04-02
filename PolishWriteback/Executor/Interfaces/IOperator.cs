using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals;

namespace SmallScript.PolishWriteback.Executor.Interfaces
{
	internal interface IOperator
	{
		IGrammarEntry GrammarEntry { get; }
		
		void Execute(RuntimeData runtimeData);
	}
}