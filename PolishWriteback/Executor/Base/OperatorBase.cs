using System;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals;
using SmallScript.PolishWriteback.Executor.Internals.Tokens;

namespace SmallScript.PolishWriteback.Executor.Base
{
	internal abstract class OperatorBase : IOperator
	{
		public abstract IGrammarEntry GrammarEntry { get; }

		public abstract void Execute(RuntimeData runtimeData);

		protected int PopIntValue(RuntimeData runtimeData)
		{
			var token = runtimeData.Stack.Pop();

			switch (token)
			{
				case IntValueToken valueToken:
				{
					return valueToken.IntValue;
				}
				case ConstantToken c:
				{
					return Int32.Parse(c.Value);
				}
				case VariableToken v:
				{
					return runtimeData.Variables.Get(v);
				}
				default:
				{
					throw new InvalidOperationException();
				}
			}
		}

		protected bool PopBoolValue(RuntimeData runtimeData)
		{
			var token = runtimeData.Stack.Pop();

			if (!(token is BoolValueToken b))
			{
				throw new InvalidOperationException();
			}

			return b.BoolValue;
		}
		
		protected void PushValue(RuntimeData runtimeData, int value)
		{
			runtimeData.Stack.Push(new IntValueToken(value));
		}
		
		protected void PushValue(RuntimeData runtimeData, bool value)
		{
			runtimeData.Stack.Push(new BoolValueToken(value));
		}
	}
}