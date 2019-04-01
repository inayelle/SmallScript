using SmallScript.WritebackGenerator.Generator.Interfaces;

namespace SmallScript.WritebackGenerator.Generator.Details.Internal
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