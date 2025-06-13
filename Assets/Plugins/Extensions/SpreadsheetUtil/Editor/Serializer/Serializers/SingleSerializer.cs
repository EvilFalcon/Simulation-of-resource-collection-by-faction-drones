using System;
using UnityEditor;

namespace Utils.Serializer.Serializers
{
	public class SingleSerializer : ISerializer
	{
		public bool CanSerialize(Type type) => type == typeof(float);

		public void Serialize(object value, SerializedProperty property)
			=> property.floatValue = (float) value;
	}
}