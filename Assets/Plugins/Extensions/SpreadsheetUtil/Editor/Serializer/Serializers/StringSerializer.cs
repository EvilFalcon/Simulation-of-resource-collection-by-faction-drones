using System;
using UnityEditor;

namespace Utils.Serializer.Serializers
{
	public class StringSerializer : ISerializer
	{
		public bool CanSerialize(Type type) => type == typeof(string);

		public void Serialize(object value, SerializedProperty property)
			=> property.stringValue = (string) value;
	}
}