using UnityEngine;

namespace Plugins.Extensions.SpreadsheetUtil {
	[CreateAssetMenu(menuName = "Settings/Spreadsheet", fileName = "SpreadsheetsSettings")]
	public class SpreadsheetSettings : ScriptableObject {
		[Tooltip("Адрес макроса для скачивания таблиц")]
		public string Url = "";
		[Tooltip("Код таблицы формата '1pt8xa-a8Vxa_wdxuHymLdO7wP8CcVadu088lp8we5rk'")]
		public string Spreadsheet = "";
		public string Password = "passcode";
		public float Timeout = 20f;
	}
}

