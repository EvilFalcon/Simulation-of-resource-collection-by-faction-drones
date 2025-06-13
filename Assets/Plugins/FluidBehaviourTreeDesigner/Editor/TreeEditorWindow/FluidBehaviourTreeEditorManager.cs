using System;
using System.IO;
using FluidBehaviourTreeDesigner.Tasks;
using UnityEditor;
using UnityEngine;

namespace FluidBehaviourTreeDesigner.TreeEditorWindow
{
	/* 
	 * This manager is created by the BTInspector, the custom inspector for BehaviorTree assets.
	 * Every time the inspector receives the OnEnable message, a fresh manager is created, and
	 * destroyed when the inspector receives the OnDisable message.
	 * 
	 * Its main responsibility is to serve as the sole route for BehaviorTree manipulation.
	 * 
	 * It achieves that by exposing a selected BehaviorTree (determined by the current BehaviorTree
	 * being inspected in BTInspector) to the BTEditorWindow
	 * 
	 * BTEditorWindow manages its own sub systems to provide editing functionality, and forwards
	 * all actual manipulations to this manager.
	 * 
	 * Creation of new BehaviorTrees are also handled here.
	 */

	public class FluidBehaviourTreeEditorManager : ScriptableObject
	{
		public UnityEditor.Editor btInspector; // Inspector for the BehaviorTree asset
		public UnityEditor.Editor nodeInspector; // Inspector for selected nodes
		public FluidBehaviourTreeEditorWindow editorWindow;


		private static FluidBehaviourTreeScriptable _queuedSelectionFor;
		private static string _queuedSelectionGuid;

		public ATaskScriptable SelectedNode { get; private set; }
		public SerializedObject SelectedNodeSerializedObject { get; private set; }
		public BehaviorTreeData BehaviorTree { private set; get; }

		public FluidBehaviourTreeScriptable fluidBehaviourTreeScriptable;

		public static FluidBehaviourTreeEditorManager Manager { get; private set; }

		public static FluidBehaviourTreeEditorManager CreateInstance(
			BehaviorTreeData bt,
			FluidBehaviourTreeScriptable asset
		)
		{
			if (Manager == null)
			{
				Manager = (FluidBehaviourTreeEditorManager) CreateInstance(typeof(FluidBehaviourTreeEditorManager));
				Manager.BehaviorTree = bt;
				Manager.fluidBehaviourTreeScriptable = asset;

				// When the user clicks on a node to select it, while viewing the behavior tree associated with a GO with a BehaviorTreeAgent component
				// we store the selection then switch the Unity Editor selection over to the appropriate BTAsset. Once the Manager for the BTAsset is
				// created (here!) we perform the original selection.
				if (_queuedSelectionFor == asset)
				{
					Manager.SelectNode(bt.GetNodeByGuid(_queuedSelectionGuid));
					_queuedSelectionFor = null;
					_queuedSelectionGuid = null;
				}
			}

			return Manager;
		}

		// Lifecycle

		public void OnEnable()
		{
			hideFlags = HideFlags.HideAndDontSave;
		}

		public void OnDestroy()
		{
			Manager = null;
			// DestroyImmediate (BehaviorTree);
		}

		// Asset management ------------------------------------------------------------------------------------------------------------------------------------

		// [MenuItem("Assets/Create/Behavior Tree", false, 1)]
		static void CreateNewBehaviorTree(MenuCommand menuCommand)
		{
			string path = AssetDatabase.GetAssetPath(Selection.activeObject);
			if (path == "")
				path = "Assets";
			else if (Path.GetExtension(path) != "")
				path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

			string fullPath = AssetDatabase.GenerateUniqueAssetPath(path + "/New Behavior Tree.asset");

			var bt = new BehaviorTreeData();
			var root = CreateInstance<RootScriptable>();
			root.editorPosition = new Vector2(0, 0);
			bt.SetRoot(root);
			var fluidBehaviourTreeScriptable = CreateInstance<FluidBehaviourTreeScriptable>();
			fluidBehaviourTreeScriptable.Serialize(bt);

			AssetDatabase.CreateAsset(fluidBehaviourTreeScriptable, fullPath);
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = fluidBehaviourTreeScriptable;
		}

		public void Dirty()
		{
			if (editorWindow != null)
				editorWindow.Repaint();
			fluidBehaviourTreeScriptable.Serialize(BehaviorTree);
			EditorUtility.SetDirty(fluidBehaviourTreeScriptable);
		}

		// Behavior Tree manipulation --------------------------------------------------------------------------------------------------------------------------

		public void Add(ATaskScriptable parent, Vector2 position, Type taskType)
		{
			var node = (ATaskScriptable) CreateInstance(taskType);

			// GUID
			node.GUID = Guid.NewGuid().ToString();

			BehaviorTree.nodes.Add(node);

			// Editor Position
			if (parent != null && parent.CanConnectChild)
			{
				if (parent.ChildCount > 0)
				{
					var lastSibling = parent.Children[parent.ChildCount - 1];
					node.editorPosition = lastSibling.editorPosition + new Vector2(GridRenderer.step.x * 10, 0);
				}
				else
				{
					node.editorPosition = new Vector2(parent.editorPosition.x,
						parent.editorPosition.y + GridRenderer.step.y * 10);
				}

				parent.ConnectChild(node);
				SortChildren(parent);
			}
			else
			{
				float xOffset = position.x % GridRenderer.step.x;
				float yOffset = position.y % GridRenderer.step.y;
				node.editorPosition = new Vector2(position.x - xOffset, position.y - yOffset);
			}

			Dirty();

			// Select the newly added node
			if (editorWindow != null) SelectNode(node);
		}

		public void Connect(ATaskScriptable parent, ATaskScriptable child)
		{
			if (parent.CanConnectChild)
			{
				parent.ConnectChild(child);
				SortChildren(parent);
				Dirty();
			}
			else
			{
				Debug.LogWarning($"{parent} can't accept child {child}");
			}
		}

		public void Unparent(ATaskScriptable node)
		{
			node.Unparent();
			Dirty();
		}

		public void Delete(ATaskScriptable node)
		{
			node.Disconnect();
			BehaviorTree.nodes.Remove(node);
			DestroyImmediate(node, true);
			Dirty();
		}

		public void SetEditorPosition(ATaskScriptable node, Vector2 position)
		{
			node.editorPosition = position;
			SortChildren(node.Parent);
			Dirty();
		}

		private void SortChildren(ATaskScriptable parent)
		{
			var parentComposite = parent as ACompositeScriptable;
			if (parentComposite != null)
				parentComposite.SortChildren();
		}

		public void SelectNode(ATaskScriptable node)
		{
			// Valid selection, but BTAsset is not selected in the project view, queue selection
			if (node != null && Selection.activeObject != fluidBehaviourTreeScriptable)
			{
				_queuedSelectionFor = fluidBehaviourTreeScriptable;
				_queuedSelectionGuid = node.GUID;
				Selection.activeObject = fluidBehaviourTreeScriptable;
				return;
			}

			SelectedNode = node;
			SelectedNodeSerializedObject = node != null ? new SerializedObject(node) : null;

			// Node is null, deselecting
			if (node == null && btInspector != null)
			{
				nodeInspector = null;
				btInspector.Repaint();
				return;
			}

			// Node selected
			var newInspector = UnityEditor.Editor.CreateEditor(node);
			if (newInspector != null)
			{
				nodeInspector = newInspector;
				nodeInspector.Repaint();
			}
		}
	}
}