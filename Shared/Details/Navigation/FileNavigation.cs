namespace SmallScript.Shared.Details.Navigation
{
	public class FileNavigation
	{
		public int Line   { get; private set; }
		public int Column { get; private set; }

		public FileNavigation(int line = 1, int column = 1)
		{
			Line   = line;
			Column = column;
		}

		public void MoveLine(int offset = 1)
		{
			Line += offset;
		}

		public void MoveColumn(int offset = 1)
		{
			Column += offset;
		}

		public void SeekLine(int line)
		{
			Line = line;
		}

		public void SeekColumn(int column)
		{
			Column = column;
		}

		public FilePosition CurrentPosition => new FilePosition(Line, Column);
	}
}