using System;
using UnityEditor;

namespace Utils.Serializer.Serializers
{
	public class IntSerializer : ISerializer
	{
		public bool CanSerialize(Type type) => type == typeof(int);

		public void Serialize(object value, SerializedProperty property) 
			=> property.intValue = (int) value;
	}
}