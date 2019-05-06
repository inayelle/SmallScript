using System;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.PolishWriteback.Executor.Internals;
using SmallScript.PolishWriteback.Executor.Internals.Tokens;

namespace SmallScript.PolishWriteback.Executor.Extensions
{
	internal static class RuntimeDataExtensions
	{
		public static string Pop(this Runtime runtime)
		{
			var token = runtime.Stack.Pop();
			
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
					return runtime.Variables.Get(v).ToString();
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

		public static int PopInt(this Runtime runtime)
		{
			var token = runtime.Stack.Pop();

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
					return runtime.Variables.Get(v);
				}
				default:
				{
					throw new InvalidOperationException();
				}
			}
		}

		public static bool PopBool(this Runtime runtime)
		{
			var token = runtime.Stack.Pop();

			if (!(token is BoolValueToken b))
			{
				throw new InvalidOperationException();
			}

			return b.BoolValue;
		}
		
		public static VariableToken PopVariable(this Runtime runtime)
		{
			var token = runtime.Stack.Pop();

			if (token is VariableToken v)
			{
				return v;
			}

			throw new InvalidOperationException();
		}
		
		public static void Push(this Runtime runtime, int value)
		{
			runtime.Stack.Push(new IntValueToken(value));
		}
		
		public static void Push(this Runtime runtime, bool value)
		{
			runtime.Stack.Push(new BoolValueToken(value));
		}
	}
}