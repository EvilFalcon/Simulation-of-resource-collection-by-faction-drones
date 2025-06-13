using System;
using System.Collections.Generic;
using FluidBehaviourTreeDesigner.Tasks;

namespace FluidBehaviourTreeDesigner
{
	[Serializable]
	public class BehaviorTreeData
	{
		public string Name { set; get; }
		public RootScriptable rootNode;
		public List<ATaskScriptable> nodes = new List<ATaskScriptable>();

		public void SetRoot(RootScriptable root)
		{
			rootNode = root;
			 nodes.Add(root);
		}

		public ATaskScriptable GetNodeByGuid(string guid)
		{
			foreach (var node in nodes)
				if (node.GUID == guid)
					return node;
			return null;
		}
	}
}