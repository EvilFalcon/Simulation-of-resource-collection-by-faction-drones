using System;
using UnityEditor;

namespace Utils.Serializer
{
	public interface ISerializer
	{
		bool CanSerialize(Type type);

		void Serialize(object value, SerializedProperty property);
	}
}