using System;
using System.Linq;
using SmallScript.LexicalParsers.RegexParser.Parser.Details;
using SmallScript.LexicalParsers.RegexParser.Parser.Details.Internal;
using SmallScript.Shared.Tests;
using Xunit;

namespace SmallScript.LexicalParsers.RegexParser.Tests
{
	public class SourceCodeSplitterTest : SmallScriptTestBase
	{
		private readonly SourceCodeSplitter _splitter;

		public SourceCodeSplitterTest()
		{
			_splitter = new SourceCodeSplitter();
		}

		[Fact]
		public void TestSplitLines()
		{
			const int    expectedLineCount = 4;
			const string text              = "qwe\nasd\nzxc\n123 321";

			var entries = _splitter.SplitByLines(text);

			Assert.Equal(expectedLineCount, entries.Count);
			Assert.Collection(entries,
					item1 => Assert.Equal("qwe\n", item1),
					item1 => Assert.Equal("asd\n", item1),
					item1 => Assert.Equal("zxc\n", item1),
					item1 => Assert.Equal("123 321\n", item1));
		}

		[Fact]
		public void TestSplitTokens()
		{
			const int expectedTokenCount = 51;
			const string text = "@decl{\n" +
			                    "declare var as int of 322\n" +
			                    "}\n" +
			                    "@impl{\n" +
			                    "read var.\n" +
			                    "write var.\n" +
			                    "for var by 1 to 10 do {\n" +
			                    "if [ 1 > 5] then do {\n" +
			                    "write var.\n" +
			                    "}fi\n" +
			                    "}\n";

			var entries = _splitter.SplitByTokens(text);

			Assert.NotEmpty(entries);
			Assert.False(entries.Any(String.IsNullOrEmpty));
			Assert.Equal(expectedTokenCount, entries.Count);
		}
	}
}