using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.PolishWriteback.Generator.Interfaces;
using SmallScript.Shared.Extensions;

namespace SmallScript.PolishWriteback.Generator.Internals
{
	internal class DefaultOperationProvider : IOperationProvider
	{
		private const string OperationsNamespace = "SmallScript.PolishWriteback.Generator.Internals.Operations";
		
		public IEnumerable<IOperation> Operations
		{
			get
			{
				return typeof(DefaultOperationProvider).Assembly
				                                       .GetTypes(OperationsNamespace)
				                                       .Select(t => Activator.CreateInstance(t) as IOperation)
				                                       .ToList();
			}
		}
	}
}