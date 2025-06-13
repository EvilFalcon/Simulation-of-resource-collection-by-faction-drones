using System;
using System.Reflection;

namespace Plugins.ObjectDrawer
{
	public interface IFieldDrawer
	{
		bool CanDraw(Type type);

		bool Draw(Type type, FieldInfo fieldInfo, object instance);
	}
}