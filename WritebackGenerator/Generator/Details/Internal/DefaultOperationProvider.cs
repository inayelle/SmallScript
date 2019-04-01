using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Shared.Extensions;
using SmallScript.WritebackGenerator.Generator.Interfaces;

namespace SmallScript.WritebackGenerator.Generator.Details.Internal
{
	internal class DefaultOperationProvider : IOperationProvider
	{
		private const string OperationsNamespace =
				"SmallScript.WritebackGenerator.Generator.Details.Internal.Operations";
		
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