using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Interfaces;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class LabelOperator : IOperator
	{
		public IGrammarEntry GrammarEntry { get; } = new Terminal("@LBL");
		
		public void Execute(RuntimeData runtimeData)
		{
			//так надо
		}
	}
}