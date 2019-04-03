using System.Collections.Generic;
using SmallScript.Calculator.Interfaces;

namespace SmallScript.Calculator.Internals.Operations
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