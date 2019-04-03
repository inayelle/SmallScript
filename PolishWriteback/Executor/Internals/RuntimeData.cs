using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.Shared.Details.Auxiliary;

namespace SmallScript.PolishWriteback.Executor.Internals
{
	internal class RuntimeData
	{
		public TokenIterator Iterator { get; private set; }
		public Stack<IToken> Stack    { get; private set; }

		public IInputOutput  InputOutput { get; private set; }
		public VariablesData Variables   { get; private set; }

		private RuntimeData()
		{
		}

		public RuntimeData(TokenIterator            iterator,
		                   Stack<IToken>            stack,
		                   IInputOutput             inputOutput,
		                   VariablesData variables)
		{
			Iterator    = Require.NotNull(iterator, nameof(iterator));
			Stack       = Require.NotNull(stack, nameof(stack));
			InputOutput = Require.NotNull(inputOutput, nameof(inputOutput));
			Variables   = Require.NotNull(variables, nameof(variables));
		}

		public static RuntimeBuilder Builder { get; } = new RuntimeBuilder();

		internal class RuntimeBuilder
		{
			private RuntimeData _instance;

			public RuntimeBuilder()
			{
				_instance = new RuntimeData();
			}

			public RuntimeBuilder UseIterator(TokenIterator iterator)
			{
				_instance.Iterator = iterator;
				return this;
			}

			public RuntimeBuilder UseStack(Stack<IToken> stack)
			{
				_instance.Stack = stack;
				return this;
			}

			public RuntimeBuilder UseIo(IInputOutput inputOutput)
			{
				_instance.InputOutput = inputOutput;
				return this;
			}

			public RuntimeBuilder UseVariableTable(VariablesData variables)
			{
				_instance.Variables = variables;
				return this;
			}

			public RuntimeData Build()
			{
				var instance = _instance;

				_instance = new RuntimeData();

				return instance;
			}
		}
	}
}