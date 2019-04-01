using SmallScript.Grammars.Shared.Details;
using SmallScript.Shared.Base;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using Xunit;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Tests
{
	public class PairTest : SmallScriptTestBase
	{
		[Fact]
		public void TestEquals()
		{
			var first = new Pair(new Terminal("a"), new Terminal("b"));
			var last = new Pair(new Terminal("a"), new Terminal("b"));
			
			Assert.Equal(first, last);
			
			Assert.Equal(first.GetHashCode(), last.GetHashCode());
		}
	}
}