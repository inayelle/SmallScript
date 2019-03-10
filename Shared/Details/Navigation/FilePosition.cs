namespace SmallScript.Shared.Details.Navigation
{
	public struct FilePosition
	{
		public int Line   { get; }
		public int Column { get; }

		public FilePosition(int line, int column)
		{
			Line   = line;
			Column = column;
		}

		public override string ToString()
		{
			return $"{Line};{Column}";
		}
	}
}