using SmallScript.Shared.Details.Errors;

namespace SmallScript.SyntaxParsers.Shared.Details
{
	public class SyntaxParserResult
	{
		public static SyntaxParserResult Successful => new SyntaxParserResult();

		public bool       Ok    { get; }
		public ParseError Error { get; }

		public SyntaxParserResult()
		{
			Ok    = true;
			Error = null;
		}

		public SyntaxParserResult(ParseError error)
		{
			Ok    = false;
			Error = error;
		}
	}
}