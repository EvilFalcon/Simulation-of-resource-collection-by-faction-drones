using System;
using System.Collections.Generic;
using InstallerGenerator.Enums;

namespace InstallerGenerator.Models
{
	public class EcsInstallerTemplate
	{
		public readonly ExecutionType Type;
		public readonly Dictionary<ExecutionPriority, List<TypeElement>> Container;
		public readonly List<string> Namespaces = new List<string>();

		public string Name => $"{Type}EcsSystems";

		public string GeneratedInstallerCode;
		public int Counter;


		public EcsInstallerTemplate(ExecutionType type)
		{
			Type = type;
			Container = GetContainer();
		}

		private static Dictionary<ExecutionPriority, List<TypeElement>> GetContainer()
		{
			var priorities = Enum.GetValues(typeof(ExecutionPriority)) as ExecutionPriority[];
			var dictionary = new Dictionary<ExecutionPriority, List<TypeElement>>();
			for (var i = 0; i < priorities.Length; i++)
			{
				var priority = priorities[i];
				dictionary.Add(priority, new List<TypeElement>());
			}

			return dictionary;
		}

		protected bool Equals(EcsInstallerTemplate other)
		{
			return Type == other.Type;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((EcsInstallerTemplate)obj);
		}

		public override int GetHashCode()
		{
			return (int)Type;
		}
	}
}