using System;
using System.Collections;
using UnityEditor;

namespace Utils.Serializer.Serializers
{
	public class ArrayOrListSerializer : ISerializer
	{
		public bool CanSerialize(Type type) => type.IsArray || type.HasInterface<IList>();

		public void Serialize(object value, SerializedProperty property) 
			=> SerializedPropertySerializer.SerializeArrayProperty((IEnumerable) value, property);
	}
}