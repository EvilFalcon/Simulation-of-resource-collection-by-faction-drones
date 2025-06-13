using System.Reflection;
using FluidBehaviourTreeDesigner.Tasks;
using UnityEditor;
using UnityEngine;

namespace FluidBehaviourTreeDesigner.TreeEditorWindow
{
	public class ContextMenu
	{
		private readonly GenericMenu _menu = new GenericMenu();

		private readonly ATaskScriptable _node;
		private readonly Vector2 _point;

		public ContextMenu(ATaskScriptable node, Vector2 point)
		{
			_node = node;
			_point = point;
		}


		public void DisplayAdd(GenericMenu.MenuFunction2 add)
		{
			if (_node == null || _node.CanConnectChild)
			{
				// Add new node
				var addMsg = (_node == null ? "Add" : "Add Child") + "/";
				// List all available node subclasses
				var tasks = FluidBehaviourTreeEditorUtils.GetAllTasks();
				foreach (var task in tasks)
				{
					var menuItemName = addMsg;
					if (typeof(AActionScriptable).IsAssignableFrom(task.Type))
					{
						menuItemName += "Actions/";
					}

					if (typeof(ADecoratorBaseScriptable).IsAssignableFrom(task.Type))
					{
						menuItemName += "Decorators/";
					}

					if (typeof(ACompositeScriptable).IsAssignableFrom(task.Type))
					{
						menuItemName += "Composites/";
					}

					var menuGroupAttribute = task.Type.GetCustomAttribute<TaskMenuGroupAttribute>();
					if (menuGroupAttribute != null)
					{
						var group = menuGroupAttribute.Group;
						menuItemName += group + "/";
					}

					var taskName = task.Name.UppercaseFirst();
					_menu.AddItem(
						new GUIContent(menuItemName + taskName),
						false,
						add,
						new MenuAction(_node, _point, task)
					);
				}
			}
			else
			{
				_menu.AddDisabledItem(new GUIContent("Add"));
			}
		}

		public void DisplaySave(GenericMenu.MenuFunction2 save)
		{
			if (_node == null)
			{
				_menu.AddSeparator("");
				_menu.AddItem(new GUIContent("Save"), false, save, null);
			}

			_menu.AddSeparator("");
		}


		public void DisplayNodeParentActions(
			GenericMenu.MenuFunction2 unparent,
			GenericMenu.MenuFunction2 connectParent
		)
		{
			// Node actions
			if (_node != null)
			{
				// Connect/Disconnect Parent
				if (!(_node is RootScriptable))
				{
					if (_node.Parent != null)
						_menu.AddItem(new GUIContent("Disconnect from Parent"), false, unparent, new MenuAction(_node));
					else
						_menu.AddItem(new GUIContent("Connect to Parent"), false, connectParent, new MenuAction(_node));
				}
			}
		}

		public void DisplayNodeConnectChildAction(GenericMenu.MenuFunction2 connectChild)
		{
			// Node actions
			if (_node != null)
			{
				// Connect Child
				if (_node.CanConnectChild)
					_menu.AddItem(new GUIContent("Connect to Child"), false, connectChild, new MenuAction(_node));
				else
					_menu.AddDisabledItem(new GUIContent("Connect to Child"));
			}
		}

		public void DisplayNodeDeleteAction(GenericMenu.MenuFunction2 delete)
		{
			// Node actions
			if (_node != null)
			{
				// Deleting
				if (_node is RootScriptable)
					_menu.AddDisabledItem(new GUIContent("Delete"));
				else
					_menu.AddItem(new GUIContent("Delete"), false, delete, new MenuAction(_node));
			}
		}

		public void AddSeparator()
		{
			_menu.AddSeparator("");
		}

		public void DisplayDropDown()
		{
			_menu.DropDown(new Rect(_point.x, _point.y, 0, 0));
		}
	}

	public class MenuAction
	{
		public readonly TaskType TaskType;
		public Vector2 Position;
		public readonly ATaskScriptable Node;

		public MenuAction(ATaskScriptable nodeVal)
		{
			Node = nodeVal;
		}

		public MenuAction(ATaskScriptable nodeVal, Vector2 positionVal, TaskType taskType)
		{
			Node = nodeVal;
			Position = positionVal;
			TaskType = taskType;
		}
	}
}