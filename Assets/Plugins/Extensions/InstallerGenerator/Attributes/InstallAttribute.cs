using System;
using InstallerGenerator.Enums;

namespace InstallerGenerator.Attributes {
	public class InstallAttribute : Attribute {
		public readonly ExecutionType Type;
		public readonly ExecutionPriority Priority;
		public readonly int Order;
		public readonly string[] Features;

		public InstallAttribute(
			ExecutionType type,
			ExecutionPriority priority,
			int order,
			params string[] features
		) {
			Type     = type;
			Priority = priority;
			Order    = order;
			Features     = features;
		}
	}
}
