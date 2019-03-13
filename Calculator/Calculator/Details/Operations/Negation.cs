using System.Collections.Generic;
using SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Details.Operations
{
	internal class Negation : IOperation
	{
		public int Priority => 4;
		
		public void Perform(Stack<double> tokens)
		{
			var result = -tokens.Pop();
			
			tokens.Push(result);
		}
	}
}