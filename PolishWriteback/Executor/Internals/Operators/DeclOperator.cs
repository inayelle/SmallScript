using System.Runtime.CompilerServices;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class DeclOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Declare);

		protected override void ExecuteImpl(Runtime runtime)
		{
			var value = runtime.PopInt();
			var token = runtime.PopVariable();

			runtime.Variables.Create(token, value);
		}
	}
}