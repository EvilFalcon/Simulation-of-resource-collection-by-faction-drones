using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluidBehaviourTreeDesigner.Tasks
{
	[Serializable]
	public abstract class ACompositeScriptable : ATaskScriptable
	{
				
		// Child connections
		[SerializeField] private List<ATaskScriptable> _children = new List<ATaskScriptable>();
		
		public override void ConnectChild(ATaskScriptable child) {
			_children.Add (child);
			child.Parent = this;
		}
		
		public override void DisconnectChild(ATaskScriptable child) {
			if (_children.Contains(child)) {
				_children.Remove (child);
				child.Parent = null;
			} else {
				throw new InvalidOperationException($"{child} is not a child of {this}");
			}
		}

		public void SortChildren() {
			_children.Sort ();
		}
		
		public override List<ATaskScriptable> Children => _children;

		public override int ChildCount => _children.Count;

		public override bool CanConnectChild => true;

		public override bool ContainsChild(ATaskScriptable child) { return _children.Contains (child); }
	}
}