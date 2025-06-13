using UnityEngine;

namespace Utils.Drawers.Key
{
	public class KeyValueAttribute : PropertyAttribute
	{
		public readonly string PropertyName;

		public KeyValueAttribute(string propertyName)
		{
			PropertyName = propertyName;
		}
	}
}