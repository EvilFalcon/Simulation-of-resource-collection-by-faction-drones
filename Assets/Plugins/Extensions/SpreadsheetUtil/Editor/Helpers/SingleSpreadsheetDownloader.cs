using System;
using UnityEditor;
using UnityEngine;

namespace SpreadsheetUtil.Helpers
{
	public abstract class SingleSpreadsheetDownloader : SpreadsheetDownloader, ISpreadsheetLoader
	{
		protected abstract string SpreadsheetName { get; }

		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Download"))
				DownloadAndProcess();

			base.OnInspectorGUI();
		}

		public event Action<bool> Success;

		public void DownloadAndProcess()
		{
			EditorUtility.DisplayProgressBar("Download", $"Table: {SpreadsheetName}...", 0);
			try
			{
				Load(SpreadsheetName, OnComplete);
			}
			catch (Exception e)
			{
				EditorUtility.ClearProgressBar();
				UnityEngine.Debug.LogError($"[SingleSpreadsheetDownloader] <b>{SpreadsheetName}:</b> {e}");
			}
		}

		private void OnComplete(string json)
		{
			EditorUtility.DisplayProgressBar("Download", $"Table: {SpreadsheetName}...", .5f);
			var success = false;
			try
			{
				Serialize(json);
				serializedObject.ApplyModifiedProperties();
				success = true;
			}
			finally
			{
				EditorUtility.ClearProgressBar();
				Success?.Invoke(success);
			}
		}

		protected abstract void Serialize(string json);
	}
}