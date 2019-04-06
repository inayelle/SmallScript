using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SmallScript.DesktopUI.Details;
using SmallScript.DesktopUI.Details.Enums;
using SmallScript.DesktopUI.Details.Logic;
using SmallScript.DesktopUI.Details.Theming;
using SmallScript.DesktopUI.Interfaces;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.Shared.Details.Auxiliary;

namespace SmallScript.DesktopUI.Views
{
	public class MainWindow : Window, IView
	{
		private TabControl _tabControl;
		private TabItem    _editorTab;
		private TabItem    _tokensTab;
		private TabItem    _writebackTab;
		private TabItem    _executionTab;
		private TabItem    _historyTab;

		private MenuItem _openFileButton;
		private MenuItem _saveFileButton;
		private MenuItem _closeFileButton;
		private MenuItem _exitButton;
		private MenuItem _compileButton;
		private MenuItem _executeButton;

		private ListBox _tokenList;
		private ListBox _writebackList;
		private ListBox _historyList;
		private ListBox _outputList;

		private TextBlock _statusField;

		private TextBox _codeField;

		private NumericUpDown _fontSizeUpDown;

		public int CodeFontSize
		{
			get => throw new NotSupportedException();
			set => _codeField.FontSize = value;
		}

		public string StatusField
		{
			get => _statusField.Text;
			set => _statusField.Text = $"[{DateTime.Now:HH:mm:ss}] {value}";
		}

		public string CodeField
		{
			get => _codeField.Text;
			set => _codeField.Text = value;
		}

		public IEnumerable<string> TokenList
		{
			get => throw new NotSupportedException();
			set => _tokenList.Items = Require.NotNull(value, nameof(value));
		}

		public IEnumerable<string> WritebackList
		{
			get => throw new NotSupportedException();
			set => _writebackList.Items = value;
		}

		public IEnumerable<string> HistoryList
		{
			get => throw new NotSupportedException();
			set => _historyList.Items = value;
		}

		private readonly GraphicalIO _inputOutput;

		public IInput StandartInput
		{
			get => _inputOutput;
			set => Require.NotNull(value, nameof(value));
		}

		public IOutput StandartOutput
		{
			get => _inputOutput;
			set => Require.NotNull(value, nameof(value));
		}

		public DisplayTab ActiveTab
		{
			get => throw new NotImplementedException();
			set
			{
				TabItem item;

				switch (value)
				{
					case DisplayTab.Editor:
						item = _editorTab;
						break;
					case DisplayTab.Tokens:
						item = _tokensTab;
						break;
					case DisplayTab.SyntaxAnalysis:
						throw new NotImplementedException();
						break;
					case DisplayTab.Writeback:
						item = _writebackTab;
						break;
					case DisplayTab.Execution:
						item = _executionTab;
						break;
					case DisplayTab.History:
						item = _historyTab;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(value), value, null);
				}

				_tabControl.SelectedItem = item;
			}
		}

		public event EventHandler<string>          OnOpenFileButtonClick;
		public event EventHandler<string>          OnSaveFileButtonClick;
		public event EventHandler<RoutedEventArgs> OnCloseFileButtonClick;
		public event EventHandler<RoutedEventArgs> OnExitFileButtonClick;

		public event EventHandler<RoutedEventArgs> OnCompileButtonClick;
		public event EventHandler<RoutedEventArgs> OnExecuteButtonClick;

		public MainWindow()
		{
			_inputOutput = new GraphicalIO();

			var model     = new Model(this);
			var presenter = new Presenter(this, model);

			InitializeComponent();
			InitializeControls();
			InitializeEvents();

			DarkGreenTheme.Instance.Apply(this);

			Setup();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		private void InitializeControls()
		{
			_tabControl   = this.Find<TabControl>("TabControl");
			_editorTab    = this.Find<TabItem>("EditorTab");
			_tokensTab    = this.Find<TabItem>("TokensTab");
			_writebackTab = this.Find<TabItem>("WritebackTab");
			_executionTab = this.Find<TabItem>("ExecutionTab");
			_historyTab   = this.Find<TabItem>("HistoryTab");

			_openFileButton  = this.Find<MenuItem>("OpenFileButton");
			_closeFileButton = this.Find<MenuItem>("CloseFileButton");
			_saveFileButton  = this.Find<MenuItem>("SaveFileButton");
			_exitButton      = this.Find<MenuItem>("ExitButton");
			_compileButton   = this.Find<MenuItem>("CompileButton");
			_executeButton   = this.Find<MenuItem>("ExecuteButton");

			_tokenList     = this.Find<ListBox>("TokenList");
			_writebackList = this.Find<ListBox>("WritebackList");
			_outputList    = this.Find<ListBox>("OutputList");
			_historyList   = this.Find<ListBox>("HistoryList");

			_statusField = this.Find<TextBlock>("StatusField");
			_codeField   = this.Find<TextBox>("CodeField");

			_fontSizeUpDown = this.Find<NumericUpDown>("FontSizeUpDown");
		}

		private void InitializeEvents()
		{
			_openFileButton.Click += OpenFileButtonOnClick;
			_saveFileButton.Click += SaveFileButtonOnClick;

			_closeFileButton.Click += OnCloseFileButtonClick;
			_exitButton.Click      += OnExitFileButtonClick;

			_compileButton.Click += OnCompileButtonClick;
			_executeButton.Click += OnExecuteButtonClick;

			_fontSizeUpDown.ValueChanged += FontSizeUpDownOnValueChanged;
		}

		private void Setup()
		{
			_outputList.Items       = new List<string>();
			_inputOutput.OutputList = _outputList;

			_codeField.Text   = @"stdout << ""Hello word""";
			_statusField.Text = "Welcome back! Again...";
		}

		private async void OpenFileButtonOnClick(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog
			{
					AllowMultiple = false,
					Title         = "Open source code file...",
					Filters = new List<FileDialogFilter>
					{
							new FileDialogFilter { Extensions = new List<string> { "ss" } }
					}
			};

			var files = await dialog.ShowAsync(this);

			OnOpenFileButtonClick?.Invoke(this, files.First());
		}

		private async void SaveFileButtonOnClick(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog
			{
					AllowMultiple = false,
					Title         = "Save source code file...",
					Filters = new List<FileDialogFilter>
					{
							new FileDialogFilter { Extensions = new List<string> { "ss" } }
					}
			};

			var files = await dialog.ShowAsync(this);

			OnSaveFileButtonClick?.Invoke(this, files.First());
		}

		private void FontSizeUpDownOnValueChanged(object sender, NumericUpDownValueChangedEventArgs e)
		{
			CodeFontSize = (int) e.NewValue;
		}
	}
}