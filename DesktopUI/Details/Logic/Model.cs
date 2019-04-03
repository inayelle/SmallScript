using SmallScript.DesktopUI.Interfaces;
using SmallScript.Shared.Details.Auxiliary;

namespace SmallScript.DesktopUI.Details.Logic
{
	internal sealed class Model
	{
		private readonly IView _view;

		public Model(IView view)
		{
			_view = Require.NotNull(view, nameof(view));
		}
	}
}