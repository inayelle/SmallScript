using SmallScript.Shared.Details.Navigation;

namespace SmallScript.Shared.Details.Errors
{
	public class ParseError
	{
		public string       Message   { get; }
		public FilePosition OccuredAt { get; }

		public ParseError(string message, FilePosition occuredAt)
		{
			Message   = message;
			OccuredAt = occuredAt;
		}
	}
}