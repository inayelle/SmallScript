using SmallScript.PolishWriteback.Generator.Interfaces;

namespace SmallScript.PolishWriteback.Generator.Internals
{
	internal class LabelIdentitySource : ILabelIdentitySource
	{
		private int _id;

		public int NextLabelId => ++_id;

		private LabelIdentitySource()
		{
			_id = 0;
		}
		
		public static LabelIdentitySource Instance { get; } = new LabelIdentitySource();
	}
}