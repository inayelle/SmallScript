using System;
using System.Collections.Generic;

namespace SmallScript.PolishWriteback.Executor.Details
{
	public sealed class WritebackExecutionResult
	{
		public bool   Ok      { get; private set; }
		public string Message { get; private set; }

		public IEnumerable<HistoryPoint> History { get; private set; }

		private WritebackExecutionResult()
		{
		}

		public static WritebackExecutionResult FromHistory(IEnumerable<HistoryPoint> history)
		{
			return new WritebackExecutionResult
			{
					Ok      = true,
					Message = "Ok",
					History = history
			};
		}

		public static WritebackExecutionResult FromException(Exception exception, IEnumerable<HistoryPoint> history)
		{
			return new WritebackExecutionResult
			{
					Ok      = false,
					Message = exception.Message,
					History = history
			};
		}
	}
}