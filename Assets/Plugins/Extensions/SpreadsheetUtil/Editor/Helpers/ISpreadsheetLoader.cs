using System;

namespace SpreadsheetUtil.Helpers
{
	public interface ISpreadsheetLoader
	{
		event Action<bool> Success;
		void DownloadAndProcess();
	}
}