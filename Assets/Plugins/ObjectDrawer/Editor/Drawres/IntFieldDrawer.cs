using System;
using System.Reflection;
using UnityEditor;

namespace Plugins.ObjectDrawer.Drawres
{
	public class IntFieldDrawer : IFieldDrawer
	{
		public bool CanDraw(Type type) => type == typeof(int);

		public bool Draw(Type type, FieldInfo fieldInfo, object instance)
		{
			var value = fieldInfo.GetValue(instance);
			var newValue = EditorGUILayout.IntField(fieldInfo.Name, (int)value);
			if (value.Equals(newValue))
				return false;

			fieldInfo.SetValue(instance, newValue);
			return true;
		}
	}
}