using UnityEngine;

namespace FluidBehaviourTreeDesigner.Inspectors
{
	public class AFluidInspector : UnityEditor.Editor {

		private static GUIStyle _titleStyle;
		private static GUIStyle _descriptionStyle;
		private static GUIStyle _subtitleStyle;

		protected static GUIStyle TitleStyle {
			get {
				if (_titleStyle == null) {
					_titleStyle = new GUIStyle
					{
						fontSize = 18
					};
				}
				return _titleStyle;
			}
		}
		
		protected static GUIStyle DescriptionStyle {
			get {
				if (_descriptionStyle == null) {
					_descriptionStyle = new GUIStyle
					{
						fontSize = 14
					};
				}
				return _descriptionStyle;
			}
		}

		protected static GUIStyle SubtitleStyle {
			get {
				if (_subtitleStyle == null) {
					_subtitleStyle = new GUIStyle
					{
						fontSize = 15
					};
				}
				return _subtitleStyle;
			}
		}
	}
}