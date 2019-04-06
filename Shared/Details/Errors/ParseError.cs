using SmallScript.Shared.Details.Navigation;

namespace SmallScript.Shared.Details.Errors
{
	public class ParseError
	{
		public ParseError(string message, FilePosition occuredAt)
		{
			Message   = message;
			OccuredAt = occuredAt;
		}

		public string       Message   { get; }
		public FilePosition OccuredAt { get; }

		public override string ToString()
		{
			return $"{Message} at {OccuredAt};";
		}
	}
}