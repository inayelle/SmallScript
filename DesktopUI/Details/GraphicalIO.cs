using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.Shared.Details.Auxiliary;

namespace SmallScript.DesktopUI.Details
{
	internal sealed class GraphicalIO : IInput, IOutput
	{
		private readonly ICollection<string> _output;

		private ListBox _outputList;

		public ListBox OutputList
		{
			get => _outputList;
			set
			{
				_outputList       = Require.NotNull(value, nameof(value));
				_outputList.Items = _output;
			}
		}

		public GraphicalIO()
		{
			_output = new ObservableCollection<string>();
		}

		public int Read()
		{
			throw new System.NotImplementedException();
		}

		public void Write(string value)
		{
			_output.Add(value);
		}

		public void Clear()
		{
			_output.Clear();
		}
		
		void IOutput.Clear()
		{
			Clear();
		}
		
		void IInput.Clear()
		{
			Clear();
		}
	}
}