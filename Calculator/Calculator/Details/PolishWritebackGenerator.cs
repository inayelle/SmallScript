using System.Collections.Generic;
using SmallScript.Calculator.Extensions;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.Calculator.Details
{
	internal sealed class PolishWritebackGenerator
	{
		public IEnumerable<IToken> Generate(IEnumerable<IToken> tokens)
		{
			var result = new List<IToken>();
			var stack  = new Stack<IToken>();

			foreach (var token in tokens)
			{
				if (token is ConstantToken)
				{
					result.Add(token);
				}

				if (token is DelimiterToken)
				{
					if (token.Value.Equals("<EOL>"))
					{
						break;
					}

					if (token.Value.Equals("("))
					{
						stack.Push(token);
						continue;
					}

					if (token.Value.Equals(")"))
					{
						while (!stack.Peek().Value.Equals("("))
						{
							result.Add(stack.Pop());
						}

						stack.Pop();
						continue;
					}

					while (stack.Count > 0 && token.GetPriority() <= stack.Peek().GetPriority())
					{
						result.Add(stack.Pop());
					}

					stack.Push(token);
				}
			}

			while (stack.Count > 0)
			{
				result.Add(stack.Pop());
			}

			return result;
		}
	}
}