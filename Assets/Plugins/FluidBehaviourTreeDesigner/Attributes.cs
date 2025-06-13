using System;

namespace FluidBehaviourTreeDesigner
{
	[AttributeUsage(AttributeTargets.Class)]
	public class TaskAttribute : Attribute
	{
		public readonly string Name;
		public readonly bool DisplayInContextMenu;

		public TaskAttribute(string name)
		{
			Name = name;
			DisplayInContextMenu = true;
		}

		public TaskAttribute(string name, bool displayInContextMenu)
		{
			Name = name;
			DisplayInContextMenu = displayInContextMenu;
		}
	}
	
	[AttributeUsage(AttributeTargets.Class)]
	public class TaskDescriptionAttribute : Attribute
	{
		public readonly string Description;

		public TaskDescriptionAttribute(string description)
		{
			Description = description;
		}
	}
	
	[AttributeUsage(AttributeTargets.Class)]
	public class TaskMenuGroupAttribute : Attribute
	{
		public readonly string Group;

		public TaskMenuGroupAttribute(string group)
		{
			Group = group;
		}
	}
}