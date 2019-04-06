using System;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.PolishWriteback.Executor.Internals;
using SmallScript.PolishWriteback.Executor.Internals.Tokens;

namespace SmallScript.PolishWriteback.Executor.Extensions
{
	internal static class RuntimeDataExtensions
	{
		public static string Pop(this RuntimeData runtimeData)
		{
			var token = runtimeData.Stack.Pop();
			
			switch (token)
			{
				case IntValueToken valueToken:
				{
					return valueToken.IntValue.ToString();
				}
				case BoolValueToken boolValueToken:
				{
					return boolValueToken.BoolValue.ToString();
				}
				case ConstantToken c:
				{
					return c.Value;
				}
				case VariableToken v:
				{
					return runtimeData.Variables.Get(v).ToString();
				}
				case StringToken s:
				{
					return s.Value;
				}
				default:
				{
					throw new InvalidOperationException();
				}
			}
		}

		public static int PopInt(this RuntimeData runtimeData)
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

		public static bool PopBool(this RuntimeData runtimeData)
		{
			var token = runtimeData.Stack.Pop();

			if (!(token is BoolValueToken b))
			{
				throw new InvalidOperationException();
			}

			return b.BoolValue;
		}
		
		public static VariableToken PopVariable(this RuntimeData runtimeData)
		{
			var token = runtimeData.Stack.Pop();

			if (token is VariableToken v)
			{
				return v;
			}

			throw new InvalidOperationException();
		}
		
		public static void Push(this RuntimeData runtimeData, int value)
		{
			runtimeData.Stack.Push(new IntValueToken(value));
		}
		
		public static void Push(this RuntimeData runtimeData, bool value)
		{
			runtimeData.Stack.Push(new BoolValueToken(value));
		}
	}
}