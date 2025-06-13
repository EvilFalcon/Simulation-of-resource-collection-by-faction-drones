using System;
using FluidBehaviourTreeDesigner.Tasks;
using FluidBehaviourTreeDesigner.TreeEditorWindow;
using UnityEditor;
using UnityEngine;

namespace FluidBehaviourTreeDesigner.Inspectors
{
	[CustomEditor(typeof(FluidBehaviourTreeScriptable))]
	public class FluidBehaviourTreeInspector : AFluidInspector
	{
		protected FluidBehaviourTreeEditorManager Manager;

		public void OnEnable()
		{
			var btAsset = (FluidBehaviourTreeScriptable) serializedObject.targetObject;
			EditorUtility.DisplayProgressBar("Loading...", $"Behaviour tree: {btAsset.name}...", 50);
			try
			{
				var bt = btAsset.Deserialize();
				Manager = FluidBehaviourTreeEditorManager.CreateInstance(bt, btAsset);
				Manager.btInspector = this;
			}
			catch (Exception e)
			{
				EditorUtility.ClearProgressBar();
				Debug.LogError($"{nameof(FluidBehaviourTreeInspector)}: {e}");
			}
			finally
			{
				EditorUtility.ClearProgressBar();
			}
		}

		public void OnDisable()
		{
			DestroyImmediate(Manager);
		}

		public override void OnInspectorGUI()
		{
			var btAsset = (FluidBehaviourTreeScriptable) serializedObject.targetObject;
			var selectedNode = Manager.SelectedNode;
			if (selectedNode == null)
			{
				DrawDefaultTreeInspector(btAsset);
			}
			else
			{
				DrawSelectedNode(selectedNode);
			}

			if (GUI.changed)
			{
				Manager.Dirty();
			}
		}

		protected virtual void DrawDefaultTreeInspector(FluidBehaviourTreeScriptable btAsset)
		{
			EditorGUILayout.LabelField(btAsset.name, TitleStyle);

			if (Manager.BehaviorTree.nodes.Count > 2)
				EditorGUILayout.LabelField($"{Manager.BehaviorTree.nodes.Count - 1} nodes");
			else if (Manager.BehaviorTree.nodes.Count == 2)
				EditorGUILayout.LabelField("Empty");
			else
				EditorGUILayout.LabelField("1 node");

			EditorGUILayout.Space();

			btAsset.description = EditorGUILayout.TextArea(btAsset.description, DescriptionStyle);

			EditorGUILayout.Space();

			if (GUILayout.Button("Show Behavior Tree editor"))
			{
				FluidBehaviourTreeEditorWindow.ShowWindow();
			}
		}

		protected virtual void DrawSelectedNode(ATaskScriptable taskScriptable)
		{
			Manager.nodeInspector.OnInspectorGUI();
			if (GUI.changed)
			{
				Manager.Dirty();
			}
		}
	}
}