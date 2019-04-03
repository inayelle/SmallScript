using SmallScript.DesktopUI.Details.Logic;
using SmallScript.DesktopUI.Interfaces;
using SmallScript.Shared.Details.Auxiliary;

namespace SmallScript.DesktopUI.Details
{
	internal sealed class Presenter
	{
		private readonly IView _view;
		private readonly Model _model;

		public Presenter(IView view, Model model)
		{
			_view  = Require.NotNull(view, nameof(view));
			_model = Require.NotNull(model, nameof(model));
		}
	}
}