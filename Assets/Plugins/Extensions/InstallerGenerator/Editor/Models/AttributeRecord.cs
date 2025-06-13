using System;
using InstallerGenerator.Attributes;

namespace InstallerGenerator.Models
{
	public struct AttributeRecord
	{
		public readonly Type Type;
		public readonly InstallAttribute Attribute;
		public readonly string[] Features;
		public readonly AttributeChanges Changes;

		public AttributeRecord(Type type, InstallAttribute attribute)
		{
			Type = type;
			Attribute = attribute;
			Features = Attribute != null ? Attribute.Features : new string[0];
			Changes = new AttributeChanges
			{
				Type = attribute.Type,
				Priority = attribute.Priority,
				Name = string.Join("|", attribute.Features),
				Order = attribute.Order
			};
		}
	}
}