using System;
using System.Reflection;
using UnityEditor;

namespace Utils.Serializer.Serializers
{
	public class EnumSerializer : ISerializer
	{
		public bool CanSerialize(Type type) => type.IsEnum && type.GetCustomAttribute<FlagsAttribute>() == null;

		public void Serialize(object value, SerializedProperty property)
		{
			var type = value.GetType();
			var values = Enum.GetValues(type);
			var enumIndex = Array.IndexOf(values, value);
			property.enumValueIndex = enumIndex;
		}
	}
}