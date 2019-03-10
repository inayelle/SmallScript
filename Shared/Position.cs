namespace SmallScript.Shared
{
	public struct Position
	{
		public int Line   { get; private set; }
		public int Column { get; private set; }

		public Position(int line = 1, int column = 1)
		{
			Line   = line;
			Column = column;
		}

		public void MoveNextLine()
		{
			++Line;
		}

		public void MoveNextColumn()
		{
			++Column;
		}

		public void SeekLine(int line)
		{
			Line = line;
		}

		public void SeekColumn(int column)
		{
			Column = column;
		}
	}
}