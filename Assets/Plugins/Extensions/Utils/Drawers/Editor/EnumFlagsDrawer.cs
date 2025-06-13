using UnityEditor;
using UnityEngine;

namespace Utils.Enums.Flags
{
	[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
	public class EnumFlagsDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
		}
	}
}