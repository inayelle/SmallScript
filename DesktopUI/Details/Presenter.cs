using System;
using System.IO;
using System.Text;
using Avalonia.Interactivity;
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

			InitializeEvents();
		}

		private void InitializeEvents()
		{
			_view.OnOpenFileButtonClick  += OnOpenFileButtonClick;
			_view.OnCloseFileButtonClick += OnCloseFileButtonClick;
			_view.OnSaveFileButtonClick  += OnSaveFileButtonClick;
			_view.OnExitFileButtonClick  += OnExitFileButtonClick;

			_view.OnCompileButtonClick += OnCompileButtonClick;
			_view.OnExecuteButtonClick += OnExecuteButtonClick;
		}

		private async void OnOpenFileButtonClick(object sender, string path)
		{
			using (var reader = new StreamReader(path, Encoding.UTF8))
			{
				_view.CodeField = await reader.ReadToEndAsync();
			}
		}

		private void OnCloseFileButtonClick(object sender, RoutedEventArgs e)
		{
			_view.CodeField = "";
			_view.StandartOutput.Clear();

			_view.TokenList     = ArraySegment<string>.Empty;
			_view.WritebackList = ArraySegment<string>.Empty;
			_view.HistoryList   = ArraySegment<string>.Empty;
		}

		private async void OnSaveFileButtonClick(object sender, string path)
		{
			using (var writer = new StreamWriter(path, false, Encoding.UTF8))
			{
				await writer.WriteLineAsync(_view.CodeField);
			}
		}

		private void OnExitFileButtonClick(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private async void OnCompileButtonClick(object sender, RoutedEventArgs e)
		{
			await _model.CompileAsync();
		}

		private async void OnExecuteButtonClick(object sender, RoutedEventArgs e)
		{
			await _model.ExecuteAsync();
		}
	}
}