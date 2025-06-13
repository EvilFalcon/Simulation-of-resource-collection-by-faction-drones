using System.Collections.Generic;
using FluidBehaviourTreeDesigner.Tasks;
using UnityEditor;
using UnityEngine;

namespace FluidBehaviourTreeDesigner
{
	public class NodeRenderer
	{
		private Texture2D _nodeTexture;
		private Texture2D _nodeDebugTexture;
		private Texture2D _shadowTexture;
		private readonly Color _edgeColor = Color.white;
		private readonly Color _shadowColor = new Color(0f, 0f, 0f, 0.15f);

		// Selection
		private Texture2D _selectionTexture;
		private readonly Color _selColor = new Color(1f, .78f, .353f);
		private const float SEL_MARGIN = 2f;
		private const float SEL_WIDTH = 2f;

		private readonly Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

		public static float Width => GridRenderer.step.x * 6;
		public static float Height => GridRenderer.step.y * 6;

		public void Draw(ATaskScriptable node, bool selected)
		{
			const float shadowOffset = 3;

			// Edge
			if (node.Parent != null)
			{
				// Shadow
				var offset = new Vector2(shadowOffset, shadowOffset);
				DrawEdge(node.Parent.editorPosition + offset, node.editorPosition + offset, Width, Height,
					_shadowColor);

				// Line
				DrawEdge(node.Parent.editorPosition, node.editorPosition, Width, Height, _edgeColor);
			}

			// Node Shadow

			var nodeRect = new Rect(node.editorPosition.x, node.editorPosition.y, Width, Height);
			var shadowRect = new Rect(nodeRect.x + shadowOffset, nodeRect.y + shadowOffset, nodeRect.width,
				nodeRect.height);

			if (_shadowTexture == null)
			{
				_shadowTexture = new Texture2D(1, 1)
				{
					hideFlags = HideFlags.DontSave
				};
				_shadowTexture.SetPixel(0, 0, _shadowColor);
				_shadowTexture.Apply();
			}

			GUI.DrawTexture(shadowRect, _shadowTexture);

			// Node
			if (_nodeTexture == null)
			{
				var colA = new Color(0.765f, 0.765f, 0.765f);
				var colB = new Color(0.886f, 0.886f, 0.886f);

				_nodeTexture = new Texture2D(1, (int) Height)
				{
					hideFlags = HideFlags.DontSave
				};
				for (var y = 0; y < Height; y++) _nodeTexture.SetPixel(0, y, Color.Lerp(colA, colB, (float) y / 75));
				_nodeTexture.Apply();
			}

			// Node Debug
			if (_nodeDebugTexture == null)
			{
				var colA = new Color(1.000f, 0.796f, 0.357f);
				var colB = new Color(0.894f, 0.443f, 0.008f);

				_nodeDebugTexture = new Texture2D(1, (int) Height)
				{
					hideFlags = HideFlags.DontSave
				};
				for (var y = 0; y < Height; y++)
					_nodeDebugTexture.SetPixel(0, y, Color.Lerp(colA, colB, (float) y / 75));
				_nodeDebugTexture.Apply();
			}

			// if (node.behaviorTree.debugMode && node.behaviorTree.currentNode == node) {
			// 	GUI.DrawTexture (nodeRect, _nodeDebugTexture);
			// } else {
			GUI.DrawTexture(nodeRect, _nodeTexture);
			// }

			// Title
			DrawNodeCaption(node);

			// Icons
			DrawNodeIcon(nodeRect, node);

			// Debug status
			// DrawStatusIcon(nodeRect, node);

			// Selection highlight
			if (selected)
			{
				if (_selectionTexture == null)
				{
					_selectionTexture = new Texture2D(1, 1)
					{
						hideFlags = HideFlags.DontSave
					};
					_selectionTexture.SetPixel(0, 0, _selColor);
					_selectionTexture.Apply();
				}

				const float mbOffset = SEL_MARGIN + SEL_WIDTH; // Margin + Border offset
				GUI.DrawTexture(
					new Rect(nodeRect.x - mbOffset, nodeRect.y - mbOffset, nodeRect.width + mbOffset * 2, SEL_WIDTH),
					_selectionTexture); // Top
				GUI.DrawTexture(
					new Rect(nodeRect.x - mbOffset, nodeRect.y - SEL_MARGIN, SEL_WIDTH,
						nodeRect.height + SEL_MARGIN * 2), _selectionTexture); // Left
				GUI.DrawTexture(
					new Rect(nodeRect.x + nodeRect.width + SEL_MARGIN, nodeRect.y - SEL_MARGIN, SEL_WIDTH,
						nodeRect.height + SEL_MARGIN * 2), _selectionTexture); // Right
				GUI.DrawTexture(
					new Rect(nodeRect.x - mbOffset, nodeRect.y + nodeRect.height + SEL_MARGIN,
						nodeRect.width + mbOffset * 2, SEL_WIDTH), _selectionTexture); // Top
			}
		}

		// private void DrawStatusIcon(Rect nodeRect, ATaskScriptable node) {
		// 	EditorGUI.LabelField(new Rect(nodeRect.x, nodeRect.y + 58f, nodeRect.width, nodeRect.height), node.lastTick.ToString ());
		//
		// 	if (node.lastStatus != null) {// && FluidBehaviourTreeEditorManager.Manager.BehaviorTree.TotalTicks == node.lastTick) {
		//
		// 		string status = node.lastStatus.ToString();
		//
		// 		if (!_textures.ContainsKey (status)) {
		// 			var tex = (Texture2D) EditorGUIUtility.Load ("Assets/Plugins/FluidBehaviourTreeDesigner/Editor/Icons/Status/" + status + ".png");
		// 			if (tex == null) {
		// 				Debug.LogWarning (status + ".png not found");
		// 				return;
		// 			}
		// 			tex.hideFlags = HideFlags.DontSave;
		// 			_textures.Add (status, tex);
		// 		}
		//
		// 		var statusRect = new Rect(nodeRect.x, nodeRect.y, 32f, 32f);
		// 		GUI.DrawTexture(statusRect, _textures[status]);
		//
		// 	}
		// }

		private void DrawNodeCaption(ATaskScriptable node)
		{
			var title = FluidBehaviourTreeEditorUtils.GetTaskName(node.GetType());
			title = title.Replace(".", ".\n").UppercaseFirst();
			var textSize = GUI.skin.label.CalcSize(new GUIContent(title));
			var x = node.editorPosition.x + Width * 0.5f - textSize.x * 0.5f;
			var y = node is AActionScriptable ? node.editorPosition.y + Height : node.editorPosition.y + Height * 0.77f;
			var titleRect = new Rect(x, y, textSize.x + 10, textSize.y);
			EditorGUI.DropShadowLabel(titleRect, new GUIContent(title), EditorStyles.whiteLabel);
		}

		private void DrawNodeIcon(Rect nodeRect, ATaskScriptable node)
		{
			var width = NearestPowerOfTwo(nodeRect.width);
			var height = NearestPowerOfTwo(nodeRect.height);
			var xOffset = (nodeRect.width - width) / 2;
			var yOffset = (nodeRect.height - height) * 0.3f;
			var iconRect = new Rect(nodeRect.x + xOffset, nodeRect.y + yOffset, width, height);

			var nodeName = GetNodeName(node);
			// if (node is Sequence sequence && sequence.rememberRunning) nodeName = "MemSequence";
			// if (node is Selector selector && selector.rememberRunning) nodeName = "MemSelector";

			if (!_textures.ContainsKey(nodeName))
			{
				var tex = (Texture2D) EditorGUIUtility.Load(
					"Assets/Plugins/FluidBehaviourTreeDesigner/Editor/Icons/Nodes/" + nodeName + ".png");
				if (tex == null)
				{
					Debug.LogWarning(nodeName + ".png not found");
					return;
				}

				tex.hideFlags = HideFlags.DontSave;
				_textures.Add(nodeName, tex);
			}

			GUI.DrawTexture(iconRect, _textures[nodeName]);
		}

		private static int NearestPowerOfTwo(float value)
		{
			var result = 1;
			do
			{
				result <<= 1;
			} while (result << 1 < value);

			return result;
		}

		private static string GetNodeName(ATaskScriptable node)
		{
			switch (node)
			{
				case AActionScriptable _:
					return "action";
				default:
					return FluidBehaviourTreeEditorUtils.GetTaskName(node.GetType());
			}
		}

		private static void DrawEdge(Vector2 start, Vector2 end, float width, float height, Color color)
		{
			var offset = width / 2;
			var startPos = new Vector3(start.x + offset, start.y + height, 0);
			var endPos = new Vector3(end.x + offset, end.y, 0);
			var startTan = startPos + Vector3.up * GridRenderer.step.x * 2;
			var endTan = endPos + Vector3.down * GridRenderer.step.x * 2;
			Handles.DrawBezier(startPos, endPos, startTan, endTan, color, null, 4);
		}

		public Rect RectForNode(ATaskScriptable node, Vector2 offset)
		{
			return new Rect(node.editorPosition.x - offset.x, node.editorPosition.y - offset.y, Width, Height);
		}
	}
}