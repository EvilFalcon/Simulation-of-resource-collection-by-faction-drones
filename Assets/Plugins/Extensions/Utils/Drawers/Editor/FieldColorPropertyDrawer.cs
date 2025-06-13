using UnityEditor;
using UnityEngine;

namespace Utils
{
	[CustomPropertyDrawer(typeof(FieldColorAttribute))]
	public class FieldColorPropertyDrawer : PropertyDrawer
	{
		private Color? LabelFontColor => ((FieldColorAttribute) attribute).LabelFont;
		private Color? LabelBgColor => ((FieldColorAttribute) attribute).LabelBg;
		private Color? ContentColor => ((FieldColorAttribute) attribute).Content;
		private Color? ContentBgColor => ((FieldColorAttribute) attribute).ContentBg;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var actualLabel = EditorGUI.BeginProperty(position, label, property);
			
			DrawColorRect(position, LabelBgColor);
			DrawFieldLabel(ref position, actualLabel, LabelFontColor);
			DrawContent(position, property, ContentColor, ContentBgColor);
			
			EditorGUI.EndProperty();
		}

		private static void DrawColorRect(Rect rect, Color? color)
		{
			if (!color.HasValue)
				return;

			rect.height -= 1f;
			rect.width -= 1f;
			rect.y += 0.5f;
			rect.x -= 0.5f;
			
			EditorGUI.DrawRect(rect, color.Value);
		}
	
		private static void DrawFieldLabel(ref Rect rect, GUIContent label, Color? color)
		{
			var previousColor = GUI.color;
			GUI.color = color ?? previousColor;
		
			rect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), label);

			GUI.color = previousColor;
		}

		private static void DrawContent(
			Rect position, 
			SerializedProperty property, 
			Color? font, 
			Color? bg)
		{
			var previousFont = GUI.contentColor;
			var previousBg = GUI.backgroundColor;
		
			GUI.contentColor = font ?? previousFont;
			GUI.backgroundColor = bg ?? previousBg;

			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;
			EditorGUI.PropertyField(position, property, GUIContent.none);
			EditorGUI.indentLevel = indent;

			GUI.contentColor = previousFont;
			GUI.backgroundColor = previousBg;
		}
	}
}
