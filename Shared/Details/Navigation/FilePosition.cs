namespace SmallScript.Shared.Details.Navigation
{
	public class FilePosition
	{
		public FilePosition(int line = 1, int column = 1)
		{
			Line   = line;
			Column = column;
		}

		public int Line   { get; }
		public int Column { get; }

		public override string ToString()
		{
			return $"{Line};{Column}";
		}
	}
}