using System;
using System.Reflection;
// using UniRx;
using UnityEditor;
using UnityEngine;

namespace Utils.Serializer
{
	public static class SerializePropertyExtension
	{
		public static void SetFloat(this SerializedProperty property, string name, float value)
			=> property.FindPropertyRelative(name).floatValue = value;

		public static void SetEnum<T>(this SerializedProperty property, T value)
			where T : Enum
		{
			var stringValue = value.ToString();
			var names = Enum.GetNames(typeof(T));
			var index = -1;
			for (var i = 0; i < names.Length; i++)
			{
				if (names[i] != stringValue) continue;
				index = i;
				break;
			}

			property.enumValueIndex = index;
		}

		public static void SetEnum<T>(this SerializedProperty property, string name, T value)
			where T : Enum
		{
			var stringValue = value.ToString();
			var names = Enum.GetNames(typeof(T));
			var index = -1;
			for (var i = 0; i < names.Length; i++)
			{
				if (names[i] != stringValue) continue;
				index = i;
				break;
			}

			property.FindPropertyRelative(name).enumValueIndex = index;
		}

		public static void SetInt(this SerializedProperty property, string name, int value)
			=> property.FindPropertyRelative(name).intValue = value;
		
		public static void SetLong(this SerializedProperty property, string name, long value)
			=> property.FindPropertyRelative(name).longValue = value;

		// public static void SetLong(this SerializedProperty property, string name, LongReactiveProperty value)
		// 	=> property.FindPropertyRelative(name).FindPropertyRelative("value").longValue = value.Value;

		public static void SetString(this SerializedProperty property, string name, string value)
			=> property.FindPropertyRelative(name).stringValue = value;

		public static void SetBool(this SerializedProperty property, string name, bool value)
			=> property.FindPropertyRelative(name).boolValue = value;

		public static void SetVector3(this SerializedProperty property, string name, Vector3 value)
			=> property.FindPropertyRelative(name).vector3Value = value;

		public static Vector3 GetVector3(this SerializedProperty property, string name)
			=> property.FindPropertyRelative(name).vector3Value;

		public static void SetQuaternion(this SerializedProperty property, string name, Quaternion value)
			=> property.FindPropertyRelative(name).quaternionValue = value;

		public static Quaternion GetQuaternion(this SerializedProperty property, string name)
			=> property.FindPropertyRelative(name).quaternionValue;

		public static SerializedProperty Last(this SerializedProperty property)
			=> property.GetArrayElementAtIndex(property.arraySize - 1);

		public static void InjectField(this object obj, string fieldName, object value)
		{
			var type = obj.GetType();
			var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			field.SetValue(obj, value);
		}
	}
}