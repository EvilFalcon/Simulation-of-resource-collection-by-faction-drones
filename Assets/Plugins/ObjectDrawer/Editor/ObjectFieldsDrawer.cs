using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Plugins.ObjectDrawer.Drawres;
using UnityEngine;

namespace Plugins.ObjectDrawer
{
	public static class ObjectFieldsDrawer
	{
		private static readonly List<IFieldDrawer> FieldDrawers = new List<IFieldDrawer>
		{
			new EnumFieldDrawer(),
			new FloatFieldDrawer(),
			new IntFieldDrawer()
		};

		public static bool Draw(object obj)
		{
			var type = obj.GetType();
			var fieldInfos = type.GetFields();
			var fields = fieldInfos.Where(f => f.IsPublic || f.HasAttribute(typeof(SerializeField)));
			var changed = false;
			foreach (var field in fields)
			{
				foreach (var fieldDrawer in FieldDrawers)
				{
					var fieldType = field.FieldType;
					if (!fieldDrawer.CanDraw(fieldType))
						continue;

					changed |= fieldDrawer.Draw(fieldType, field, obj);
				}
			}

			return changed;
		}
	}
}