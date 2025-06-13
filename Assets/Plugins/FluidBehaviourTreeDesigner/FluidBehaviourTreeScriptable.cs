using System;
using System.Xml;
using FluidBehaviourTreeDesigner.Tasks;
using UnityEngine;

namespace FluidBehaviourTreeDesigner {

	[Serializable]
	public class FluidBehaviourTreeScriptable : ScriptableObject {

		public string description;
		public string serializedBehaviorTree;

		public BehaviorTreeData Deserialize()
		{
			var bt =  BehaviourTreeSerializer.Deserialize(serializedBehaviorTree);
			bt.Name = name;
			return bt;
		}

		private ATaskScriptable DeserializeSubTree(XmlElement el, BehaviorTreeData bt) =>
			BehaviourTreeSerializer.DeserializeSubTree(el, bt);
		
		public void Serialize(BehaviorTreeData behaviorTree)
		{
			serializedBehaviorTree = BehaviourTreeSerializer.Serialize(behaviorTree);
		}

		private void SerializeSubTree(ATaskScriptable node, XmlElement parentEl) {
			BehaviourTreeSerializer.SerializeSubTree(node, parentEl);
		}
	}
	
}