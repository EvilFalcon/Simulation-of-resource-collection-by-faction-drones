using System;
using System.Reflection;
using UnityEditor;

namespace Plugins.ObjectDrawer.Drawres
{
	public class EnumFieldDrawer : IFieldDrawer
	{
		public bool CanDraw(Type type) => type.IsEnum;

		public bool Draw(Type type, FieldInfo fieldInfo, object instance)
		{
			var value = fieldInfo.GetValue(instance) as Enum;
			var newValue = EditorGUILayout.EnumPopup(fieldInfo.Name, value);
			if (value.Equals(newValue))
				return false;

			fieldInfo.SetValue(instance, newValue);
			return true;
		}
	}
}