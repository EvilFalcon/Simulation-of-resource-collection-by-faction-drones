using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

namespace Plugins.Extensions.SpreadsheetUtil {
	public class SpreadsheetConnection {
		private readonly SpreadsheetSettings _settings;
		private UnityWebRequest _request;

		public SpreadsheetConnection(SpreadsheetSettings settings) { _settings = settings; }

		public IEnumerator Create(Dictionary<string, string> form, Action<string> callback) {
			form.Add("ssid", _settings.Spreadsheet);
			form.Add("pass", _settings.Password);
			_request = UnityWebRequest.Post(_settings.Url, form);
			_request.SendWebRequest();
			var begin = Time.realtimeSinceStartup;
			while (!_request.isDone) {
				if (Time.realtimeSinceStartup - begin >= _settings.Timeout) {
					Debug.Log("Time out: " + (Time.realtimeSinceStartup - begin));
					break;
				}
				yield return null;
			}
			var elapsedTime = Time.realtimeSinceStartup - begin;
			if (!string.IsNullOrEmpty(_request.error)) {
				Debug.Log(
					$"Connection error after {elapsedTime.ToString(CultureInfo.InvariantCulture)} " +
					$"seconds: {_request.error} {elapsedTime}");
				yield break;
			}
			callback(_request.downloadHandler.text);
		}
	}
}
