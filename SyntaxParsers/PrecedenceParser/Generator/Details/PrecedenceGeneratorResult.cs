using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Collections;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details
{
	public class PrecedenceGeneratorResult
	{
		public bool   Ok      { get; }
		public string Message { get; }

		public PairCollection            Pairs      { get; }
		public IEnumerable<IAlternative> Duplicates { get; }

		public PrecedenceGeneratorResult(PairCollection pairs, ICollection<IAlternative> duplicates)
		{
			Pairs      = Require.NotNull(pairs, nameof(pairs));
			Duplicates = duplicates;

			Ok = true;
			Message = "OK";
			
			if (duplicates.Any())
			{
				Ok = false;
				Message = "Duplicates found";
			}

			if (pairs.Any(p => p.IsFaulty()))
			{
				Ok = false;
				Message = "Faulty pairs found";
			}
		}
	}
}