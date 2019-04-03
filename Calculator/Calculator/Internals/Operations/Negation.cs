using System.Collections.Generic;
using SmallScript.Calculator.Interfaces;

namespace SmallScript.Calculator.Internals.Operations
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