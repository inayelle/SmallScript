using System.Collections.Generic;
using SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Details.Operations
{
	internal class Substraction : IOperation
	{
		public int Priority => 1;

		public void Perform(Stack<double> tokens)
		{
			var right = tokens.Pop();
			var left  = tokens.Pop();

			var result = left - right;

			tokens.Push(result);
		}
	}
}