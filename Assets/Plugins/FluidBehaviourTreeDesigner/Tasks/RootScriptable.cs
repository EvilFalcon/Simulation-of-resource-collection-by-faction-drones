using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluidBehaviourTreeDesigner.Tasks
{
	[Serializable]
	[Task("root", false)]
	public class RootScriptable : ATaskScriptable
	{
		// Child connections
		[SerializeField] ATaskScriptable _child;
		
		public override void ConnectChild(ATaskScriptable child) {
			if (_child == null) {
				_child = child;
				child.Parent = this;
			} else {
				throw new System.InvalidOperationException(string.Format ("{0} already has a connected child, cannot connect {1}", this, child));
			}
		}
		
		public override void DisconnectChild(ATaskScriptable child) {
			if (_child == child) {
				_child = null;
				child.Parent = null;
			} else {
				throw new InvalidOperationException(string.Format ("{0} is not a child of {1}", child, this));
			}
		}
		
		public override List<ATaskScriptable> Children {
			get {
				var nodeList = new List<ATaskScriptable> {_child};
				return nodeList;
			}
		}

		public override int ChildCount => _child != null ? 1 : 0;

		public override bool CanConnectChild => _child == null;

		public override bool ContainsChild (ATaskScriptable child)
		{
			return _child == child;
		}

		// Parent Connections

		public override ATaskScriptable Parent {
			get => null;
			set => throw new System.InvalidOperationException("The Root node cannot have a parent connection");
		}
		public override void Unparent() {
			throw new System.InvalidOperationException("The Root node cannot have a parent connection");
		}

		// Runtime

		// public override Status Tick(GameObject agent, Context context)
		// {
		// 	Status result = Status.Error;
		// 	if (_child != null) {
		// 		result = behaviorTree.Tick (_child, agent, context);
		// 	}
		// 	return result;
		// }	
	}
}