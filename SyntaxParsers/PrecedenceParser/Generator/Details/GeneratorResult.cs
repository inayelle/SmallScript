using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details
{
	public class GeneratorResult
	{
		public bool           Ok    { get; }
		public PairCollection Pairs { get; }

		public GeneratorResult(PairCollection pairs)
		{
			Pairs = Require.NotNull(pairs, nameof(pairs));

			Ok = pairs.All(p => !p.IsFaulty());
		}
	}
}