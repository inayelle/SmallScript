using System.Collections.Generic;
using SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Details.Operations
{
	public class Multiplication : IOperation
	{
		public int Priority => 2;
		
		public void Perform(Stack<double> tokens)
		{
			var result = tokens.Pop() * tokens.Pop();
			
			tokens.Push(result);
		}
	}
}