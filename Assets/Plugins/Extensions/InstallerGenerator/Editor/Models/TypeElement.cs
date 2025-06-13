using System;
using InstallerGenerator.Attributes;

namespace InstallerGenerator.Models
{
	public class TypeElement
	{
		public readonly Type Type;
		public readonly int Order;
		public readonly string Name;
		public readonly bool IsDebug;

		public TypeElement(Type type, InstallAttribute attribute, bool isDebug)
		{
			Type = type;
			Order = attribute.Order;
			Name = string.Join("|", attribute.Features);
			IsDebug = isDebug;
		}
	}
}