using System;
using UniRx;
using UnityEditor;

namespace Utils.Serializer.Serializers
{
	public class LongReactivePropertySerializer : ISerializer
	{
		public bool CanSerialize(Type type) => type == typeof(LongReactiveProperty);

		public void Serialize(object value, SerializedProperty property) 
			=> property.FindPropertyRelative("value").longValue = ((LongReactiveProperty)value).Value;
	}
}