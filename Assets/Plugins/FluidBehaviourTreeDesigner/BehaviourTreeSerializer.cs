using System.Xml;
using FluidBehaviourTreeDesigner.Tasks;
using UnityEngine;

namespace FluidBehaviourTreeDesigner
{
	public class BehaviourTreeSerializer
	{
		public static BehaviorTreeData Deserialize(string serializedBehaviorTree)
		{
			var doc = new XmlDocument();
			doc.LoadXml(serializedBehaviorTree);

			// Behavior Tree
			var bt = new BehaviorTreeData();

			// Root
			var rootEl = (XmlElement) doc.GetElementsByTagName("root").Item(0);
			var root = (RootScriptable) DeserializeSubTree(rootEl, bt);
			bt.SetRoot(root);

			// Unparented nodes
			var unparentedRoot = (XmlElement) doc.GetElementsByTagName("unparented").Item(0);
			foreach (XmlNode xmlNode in unparentedRoot.ChildNodes)
			{
				if (xmlNode is XmlElement el)
					DeserializeSubTree(el, bt);
			}

			return bt;
		}

		public static ATaskScriptable DeserializeSubTree(XmlElement el, BehaviorTreeData bt)
		{
			var name = el.Name.Replace("_", " ");
			var type = FluidBehaviourTreeEditorUtils.GetTaskTypeByName(name);
			ATaskScriptable node;
			if (type == null)
			{
				UnityEngine.Debug.LogError(
					$"[{nameof(BehaviourTreeSerializer)}] {el.Name} deserialization not implemented");
				node = ScriptableObject.CreateInstance<NullScriptable>();
			}
			else
				node = (ATaskScriptable) ScriptableObject.CreateInstance(type);

			var x = float.Parse(el.GetAttribute("x"));
			var y = float.Parse(el.GetAttribute("y"));
			node.editorPosition = new Vector2(x, y);
			node.GUID = el.GetAttribute("guid");

			node.Deserialize(el);

			// if (node is Action action) action.Deserialize(el);
			// else if (node is SequenceScriptable sequence) sequence.Deserialize(el);
			// else if (node is SelectorScriptable selector) selector.Deserialize(el);

			bt.nodes.Add(node);

			foreach (XmlNode xmlNode in el.ChildNodes)
			{
				if (xmlNode is XmlElement childEl && childEl.Name != "param")
				{
					var child = DeserializeSubTree(childEl, bt);
					node.ConnectChild(child);
				}
			}

			return node;
		}

		public static string Serialize(BehaviorTreeData behaviorTree)
		{
			// XML Document
			var doc = new XmlDocument();

			// Behavior Tree
			var btEl = doc.CreateElement("behaviortree");
			doc.AppendChild(btEl);

			// Root SubTree
			SerializeSubTree(behaviorTree.rootNode, btEl);

			// Unparented nodes root
			var unparentedEl = doc.CreateElement("unparented");
			btEl.AppendChild(unparentedEl);

			// Unparented nodes
			for (var i = 0; i < behaviorTree.nodes.Count; i++)
			{
				if (behaviorTree.nodes[i].Parent == null && !(behaviorTree.nodes[i] is RootScriptable))
				{
					SerializeSubTree(behaviorTree.nodes[i], unparentedEl);
				}
			}

			return doc.InnerXml;
		}

		public static void SerializeSubTree(ATaskScriptable node, XmlElement parentEl)
		{
			var doc = parentEl.OwnerDocument;

			var taskName = FluidBehaviourTreeEditorUtils.GetTaskName(node.GetType());
			taskName = taskName.Replace(" ", "_");
			var el = doc.CreateElement(taskName);
			el.SetAttribute("x", node.editorPosition.x.ToString());
			el.SetAttribute("y", node.editorPosition.y.ToString());
			el.SetAttribute("guid", node.GUID);

			node.Serialize(el);

			// if (node is ActionScriptable action) action.Serialize(ref el);
			// else if (node is SequenceScriptable sequence) sequence.Serialize(ref el);
			// else if (node is SelectorScriptable selector) selector.Serialize(ref el);

			parentEl.AppendChild(el);

			var count = node.ChildCount;
			for (var i = 0; i < count; i++)
			{
				SerializeSubTree(node.Children[i], el);
			}
		}
	}
}