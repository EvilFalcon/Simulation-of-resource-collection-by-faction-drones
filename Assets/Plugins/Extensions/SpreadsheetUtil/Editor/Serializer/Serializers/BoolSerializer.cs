using System;
using UnityEditor;

namespace Utils.Serializer.Serializers
{
	public class BoolSerializer: ISerializer
	{
		public bool CanSerialize(Type type) => type == typeof(bool);

		public void Serialize(object value, SerializedProperty property)
			=> property.boolValue = (bool)value;
	}
}