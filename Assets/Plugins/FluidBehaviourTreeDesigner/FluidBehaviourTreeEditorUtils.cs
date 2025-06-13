using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluidBehaviourTreeDesigner
{
	public static class FluidBehaviourTreeEditorUtils
	{
		public static List<TaskType> GetAllTasks()
		{
			var res = new List<TaskType>();
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var assembly in assemblies)
			{
				var types = assembly.GetTypes();
				foreach (var type in types)
				{
					var taskAttribute = type.GetCustomAttribute<TaskAttribute>();
					if(taskAttribute == null)
						continue;
					if(!taskAttribute.DisplayInContextMenu)
						continue;
					var task = new TaskType(type, taskAttribute.Name);
					res.Add(task);
				}
			}
			return res;
		}
		
		public static string GetTaskName(Type task)
		{
			var taskAttribute = task.GetCustomAttribute<TaskAttribute>();
			if(taskAttribute == null)
				throw new ArgumentException("taskAttribute of task " + task.Name + " is null");
			return taskAttribute.Name;
		}
		
		public static Type GetTaskTypeByName(string taskName)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var assembly in assemblies)
			{
				var types = assembly.GetTypes();
				foreach (var type in types)
				{
					var taskAttribute = type.GetCustomAttribute<TaskAttribute>();
					if(taskAttribute == null)
						continue;
					if(taskAttribute.Name.Equals(taskName))
						return type;
				}
			}
			return null;
		}
		
		public static string UppercaseFirst(this string s) => char.ToUpper(s[0]) + s.Substring(1);
	}
	

	public readonly struct TaskType
	{
		public readonly Type Type;
		public readonly string Name;

		public TaskType(Type type, string name)
		{
			Type = type;
			Name = name;
		}
	}
}