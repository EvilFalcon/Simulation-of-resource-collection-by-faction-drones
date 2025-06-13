using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace FluidBehaviourTreeDesigner.Tasks
{
	[Serializable]
	public abstract class ATaskScriptable : ScriptableObject, System.IComparable
	{
		// Editor settings
		[SerializeField]
		public Vector2 editorPosition;

		// GUID
		public string GUID;
		
		// Child connections
		public virtual void ConnectChild(ATaskScriptable child) {}
		public virtual void DisconnectChild(ATaskScriptable child) {}
		public virtual List<ATaskScriptable> Children => null;
		public virtual int ChildCount => 0;
		public virtual bool CanConnectChild => false;
		public virtual bool ContainsChild(ATaskScriptable child) { return false; }

		// IComparable for sorting left-to-right in the visual editor
		public int CompareTo(object other) {
			var otherNode = other as ATaskScriptable;
			return editorPosition.x < otherNode.editorPosition.x ? -1 : 1;
		}
		
		// Parent connections
		[SerializeField]
		private ATaskScriptable parent;
		public virtual ATaskScriptable Parent {
			get => parent;
			set {
				
				if (value == null && parent.ContainsChild(this)) {
					throw new System.InvalidOperationException(string.Format ("Cannot set parent of {0} to null because {1} still contains it in its children", this, value));
				} else if (value == null || (value != null && value.ContainsChild(this))) {
					parent = value;
				} else {
					throw new System.InvalidOperationException(string.Format ("{0} must contain {1} as a child before setting the child parent property", value, this));
				}
				
			}
		}
		public virtual void Unparent() {
			if (parent != null) {
				parent.DisconnectChild(this);
			} else {
				Debug.LogWarning(string.Format ("Attempted unparenting {0} while it has no parent"));
			}
		}

		public List<ATaskScriptable> Ancestors() {
			List<ATaskScriptable> ancestorNodes = new List<ATaskScriptable>();
			if (Parent != null) Parent.Ancestors (ref ancestorNodes);
			return ancestorNodes;
		}
		private void Ancestors(ref List<ATaskScriptable> ancestorNodes) {
			ancestorNodes.Add (this);
			if (Parent != null) Parent.Ancestors (ref ancestorNodes);
		}
		
		// All connections
		public virtual void Disconnect() {
			
			// Disconnect parent
			if (Parent != null) {
				Unparent();
			}
			
			// Disconnect children
			if (ChildCount > 0) {
				for (int i = ChildCount - 1; i >= 0; i--) {
					DisconnectChild(Children[i]);
				}
			}
		}

		// Lifecycle
		public void OnEnable() {
			hideFlags = HideFlags.HideAndDontSave;
		}

		public virtual void Serialize(XmlElement element)
		{
		}

		public virtual void Deserialize(XmlElement element)
		{
		}
	}
	
}