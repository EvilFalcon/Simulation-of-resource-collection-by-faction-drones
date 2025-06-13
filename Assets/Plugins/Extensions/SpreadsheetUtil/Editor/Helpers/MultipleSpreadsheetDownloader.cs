using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpreadsheetUtil.Helpers
{
	public abstract class MultipleSpreadsheetDownloader : SpreadsheetDownloader, ISpreadsheetLoader
	{
		protected abstract string[] SpreadsheetNames { get; }

		private Dictionary<string, string> _downloaded;

		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Download"))
				DownloadAndProcess();

			base.OnInspectorGUI();
		}

		public event Action<bool> Success;

		public void DownloadAndProcess()
		{
			_downloaded = new Dictionary<string, string>();
			LoadSpreadsheet(0);
		}

		private void LoadSpreadsheet(int index)
		{
			if (index == SpreadsheetNames.Length)
			{
				OnLoadCompleted();
				return;
			}

			var tableName = SpreadsheetNames[index];
			var progress = index / (float) SpreadsheetNames.Length;
			EditorUtility.DisplayProgressBar("Download", $"Table: {tableName}...", progress);
			try
			{
				Load(tableName, json =>
				{
					_downloaded.Add(tableName, json);
					LoadSpreadsheet(index + 1);
				});
			}
			catch (Exception e)
			{
				EditorUtility.ClearProgressBar();
				UnityEngine.Debug.LogError($"[MultipleSpreadsheetDownloader] <b>{SpreadsheetNames}:</b> {e}");
			}
		}

		private void OnLoadCompleted()
		{
			EditorUtility.DisplayProgressBar("Download", $"Serialize data...", .95f);
			var success = false;
			try
			{
				Serialize(_downloaded);
				serializedObject.ApplyModifiedProperties();
				success = true;
			}
			catch (Exception e)
			{
				UnityEngine.Debug.LogError($"[MultipleSpreadsheetDownloader] {e}");
			}
			finally
			{
				EditorUtility.ClearProgressBar();
				Success?.Invoke(success);
			}
		}

		protected abstract void Serialize(Dictionary<string, string> jsonByTables);
	}
}