using System;
using UnityEditor;
using Utils.Serializer;

namespace SpreadsheetUtil.Serializer.Serializers
{
	public class ClassOrStructSerializer : ISerializer
	{
		public bool CanSerialize(Type type) => type.IsClass || type.IsValueType;

		public void Serialize(object value, SerializedProperty property)
		{
			SerializedPropertySerializer.SerializeNonPrimitiveType(value, value.GetType(), property);
		}
	}
}