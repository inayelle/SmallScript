using System;
using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Extensions;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Details.Operations;
using SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Details
{
	public class Calculator
	{
		private readonly IDictionary<string, IOperation> _operations;
		private readonly PolishWritebackGenerator        _generator;

		public Calculator()
		{
			_generator = new PolishWritebackGenerator();
			_operations = new Dictionary<string, IOperation>(5)
			{
					{ "+", new Addition() },
					{ "-", new Substraction() },
					{ "*", new Multiplication() },
					{ "/", new Division() },
					{ "**", new Power() }
			};
		}

		public double Evaluate(IEnumerable<IToken> tokens)
		{
			var postfixTokens = _generator.Generate(tokens);
			var stack = new Stack<double>();

			foreach (var token in postfixTokens)
			{
				if (token is ConstantToken)
				{
					stack.Push(Double.Parse(token.Value));
				}
				else
				{
					_operations[token.Value].Perform(stack);
				}
			}

			return stack.Pop();
		}
	}
}