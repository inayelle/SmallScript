namespace TestPolygon
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Test(null);
		}

		private static void Test(string a)
		{
			Require.NotNull(a);
		}
	}
}