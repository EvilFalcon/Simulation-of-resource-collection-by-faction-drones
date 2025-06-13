using System;

namespace Utils.Serializer
{
	public static class ReflectionExtensions
	{
		public static bool HasInterface<T>(this Type type) 
			=> type.GetInterface(typeof(T).Name) != null;
	}
}