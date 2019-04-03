using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Errors;

namespace SmallScript.SyntaxParsers.Shared.Details
{
	public class SyntaxParseResult
	{
		public static SyntaxParseResult Successful => new SyntaxParseResult();

		public bool       Ok    { get; }
		public ParseError Error { get; }

		public SyntaxParseResult()
		{
			Ok    = true;
			Error = null;
		}

		public SyntaxParseResult(ParseError error)
		{
			Ok    = false;
			Error = Require.NotNull(error, nameof(error));
		}
	}
}