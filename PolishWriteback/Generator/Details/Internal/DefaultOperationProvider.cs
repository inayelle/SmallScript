using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.PolishWriteback.Generator.Interfaces;
using SmallScript.Shared.Extensions;

namespace SmallScript.PolishWriteback.Generator.Details.Internal
{
	internal class DefaultOperationProvider : IOperationProvider
	{
		private const string OperationsNamespace = "SmallScript.PolishWriteback.Generator.Details.Internal.Operations";
		
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