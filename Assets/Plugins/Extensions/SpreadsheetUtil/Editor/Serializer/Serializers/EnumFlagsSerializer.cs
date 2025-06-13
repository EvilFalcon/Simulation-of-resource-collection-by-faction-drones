using System;
using System.Reflection;
using UnityEditor;

namespace Utils.Serializer.Serializers
{
	public class EnumFlagsSerializer : ISerializer
	{
		public bool CanSerialize(Type type) => type.IsEnum && type.GetCustomAttribute<FlagsAttribute>() != null;

		public void Serialize(object value, SerializedProperty property) 
			=> property.intValue = (int) value;
	}
}