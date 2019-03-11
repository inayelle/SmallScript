using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.LexicalParsers.RegexParser.Parser.Details.Internal
{
	internal class IdentitySource : IIdentitySource
	{
		private int _constantId;
		private int _variableId;

		public int NextVariableId => ++_variableId;
		public int NextConstantId => ++_constantId;

		public void Reset()
		{
			_variableId = 0;
			_constantId = 0;
		}
	}
}