using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SpreadsheetUtil.Serializer.Serializers;
using UnityEditor;
using Utils.Serializer.Serializers;

namespace Utils.Serializer
{
	public static class SerializedPropertySerializer
	{
		private const BindingFlags FIELD_BINDING = BindingFlags.Instance
		                                           | BindingFlags.Public
		                                           | BindingFlags.NonPublic;

		private static readonly List<ISerializer> Serializers = new List<ISerializer>
		{
			new IntSerializer(),
			new BoolSerializer(),
			new StringSerializer(),
			new EnumSerializer(),
			new EnumFlagsSerializer(),
			new ArrayOrListSerializer(),
			new LongReactivePropertySerializer(),
			new SingleSerializer(),
			new ClassOrStructSerializer()
		};


		public static void SerializeArrayProperty(IEnumerable array, SerializedProperty arrayProperty)
		{
			var currentIndex = 0;
			arrayProperty.arraySize = 0;

			if (array == null)
				return;

			foreach (var item in array)
			{
				arrayProperty.arraySize++;
				var element = arrayProperty.GetArrayElementAtIndex(currentIndex++);
				var type = item.GetType();
				if (IsPrimitive(type))
					SerializeValueProperty(item, type, element);
				else
					SerializeNonPrimitiveType(item, type, element);
			}
		}

		private static bool IsPrimitive(Type type) => type.IsPrimitive || type == typeof(string) || type.IsEnum;

		public static void SerializeNonPrimitiveType(object item, Type type, SerializedProperty element)
		{
			var fieldsInfo = type.GetFields(FIELD_BINDING);
			var serializedFields = fieldsInfo.Where(field => !field.IsNotSerialized || field.IsPublic);
			foreach (var serializedField in serializedFields)
			{
				var value = serializedField.GetValue(item);
				var property = element.FindPropertyRelative(serializedField.Name);
				if (property == null)
					continue;

				SerializeValueProperty(value, serializedField.FieldType, property);
			}
		}

		public static void SerializeValueProperty(object value, Type type, SerializedProperty property)
		{
			var serializer = FindSerializer(type);
			if (serializer == null)
				throw new System.Exception($"[ArraySerialize] Can't serialize property {type.Name} with type {type}");
			serializer.Serialize(value, property);
		}

		private static ISerializer FindSerializer(Type serializedType)
			=> Serializers.Find(f => f.CanSerialize(serializedType));
	}
}