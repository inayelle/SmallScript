using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.Shared.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals
{
	internal class OperatorProvider
	{
		private const string OperatorsNamespace = "SmallScript.PolishWriteback.Executor.Internals.Operators";

		public IEnumerable<IOperator> Operators { get; }

		public OperatorProvider() : this(OperatorsNamespace)
		{
		}

		public OperatorProvider(string @namespace)
		{
			Operators = typeof(OperatorProvider).Assembly
			                                    .GetTypes(@namespace)
			                                    .Select(t => Activator.CreateInstance(t) as IOperator);
		}
	}
}