using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Extensions;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.WritebackGenerator.Generator.Details.Internal;
using SmallScript.WritebackGenerator.Generator.Details.Internal.Tokens;
using SmallScript.WritebackGenerator.Generator.Interfaces;

namespace SmallScript.WritebackGenerator.Generator.Details
{
	public class WritebackGenerator
	{
		private readonly IDictionary<IGrammarEntry, IOperation> _operations;

		public WritebackGenerator() : this(new DefaultOperationProvider())
		{
		}

		public WritebackGenerator(IOperationProvider operationProvider)
		{
			Require.NotNull(operationProvider, nameof(operationProvider));

			_operations = operationProvider.Operations
			                               .ToDictionary(k => k.GrammarEntry);

			foreach (var operation in _operations.Values)
			{
				operation.Operations = _operations;
			}
		}

		public IToken[] Generate(IEnumerable<IToken> tokens)
		{
			var outputTokens = new List<IToken>();
			var iterator     = new TokenIterator(tokens);
			var stack        = new Stack<IToken>();

			while (iterator.IsValid)
			{
				var currentEntry = iterator.Current.GrammarEntry;

				var operation = _operations[currentEntry];

				outputTokens.AddRange(operation.Consume(iterator, stack));
			}

			return RemoveUnnessessaryTokens(outputTokens);
		}

		private IToken[] RemoveUnnessessaryTokens(IEnumerable<IToken> tokens)
		{
			var tokenArray = tokens.Where(t => !t.IsDelimiter(">>"))
			                       .Where(t => !t.IsDelimiter("<<"))
			                       .ToArray();

			for (var i = 0; i < tokenArray.Length; ++i)
			{
				if (tokenArray[i] is LabelToken label)
				{
					label.TargetTokenOrder = i + 1;
				}
			}

			return tokenArray;
		}
	}
}