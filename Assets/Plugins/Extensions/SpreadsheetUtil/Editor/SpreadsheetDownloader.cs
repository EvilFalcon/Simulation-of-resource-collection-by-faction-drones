using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Plugins.Extensions.SpreadsheetUtil;
using Plugins.Extensions.SpreadsheetUtil.Models;
using UnityEditor;
using UnityEngine;

namespace SpreadsheetUtil
{
	public class SpreadsheetDownloader : Editor {
		private static readonly StringBuilder Builder = new StringBuilder();
		
		private IEnumerator _enumerator;
		private Action<string> _onComplete;

		protected void Load(string sheet, Action<string> onComplete) {
			_onComplete = onComplete;
			StartCoroutine(Connect(sheet));
		}

		private IEnumerator Connect(string sheet) {
			var settingsObject = AssetDatabase.FindAssets("SpreadsheetsSettings");
			if (settingsObject.Length == 0)
				throw new Exception("Create SpreadsheetsSettings with name \"SpreadsheetsSettings\"");
			var settings = AssetDatabase.LoadAssetAtPath<SpreadsheetSettings>(AssetDatabase.GUIDToAssetPath(settingsObject[0]));
			var connector = new SpreadsheetConnection(settings);
			var spreadsheet = new Spreadsheet(connector);
			return spreadsheet.GetTable(sheet, OnDownload);
		}
		
		private void StartCoroutine(IEnumerator enumerator) {
			_enumerator = enumerator;
			EditorApplication.update += Coroutine;
		}

		private void Coroutine() {
			if (_enumerator.MoveNext()) {
				if (_enumerator.Current != null)
					_enumerator = (IEnumerator) _enumerator.Current;
			}
			else { EditorApplication.update -= Coroutine; }
		}

		private void OnDownload(string result) {
			_onComplete(result);
		}

		protected static List<T> Read<T>(string json) {
			json = Builder.Append("{ \"List\":").Append(json).Append("}").ToString();
			Builder.Clear();
			return JsonUtility.FromJson<JsonItems<T>>(json).List;
		}
		
		protected void Save() {
			serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(target);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}
