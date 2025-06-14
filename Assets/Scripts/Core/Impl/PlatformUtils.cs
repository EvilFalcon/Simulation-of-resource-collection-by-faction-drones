using UnityEngine;

namespace Core
{
	public static class PlatformUtils
	{
#if UNITY_IPHONE
		public static bool IsIPhone() => Application.platform == RuntimePlatform.IPhonePlayer
		                                 || Application.platform == RuntimePlatform.OSXEditor
		                                 || Application.platform == RuntimePlatform.WindowsEditor;
#else
		public static bool IsIPhone() => Application.platform == RuntimePlatform.IPhonePlayer;
#endif

#if UNITY_ANDROID
		public static bool IsAndroid() => Application.platform == RuntimePlatform.Android
		                                 || Application.platform == RuntimePlatform.OSXEditor
		                                 || Application.platform == RuntimePlatform.WindowsEditor;
#else
		public static bool IsAndroid() => Application.platform == RuntimePlatform.Android;
#endif

		public static bool IsEditor() => Application.platform == RuntimePlatform.OSXEditor
		                                 || Application.platform == RuntimePlatform.WindowsEditor;
	}
}