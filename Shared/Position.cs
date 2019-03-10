namespace SmallScript.Shared
{
	public struct Position
	{
		public int Column { get; }
		public int Line   { get; }

		public Position(int column, int line)
		{
			Column = column;
			Line   = line;
		}
	}
}