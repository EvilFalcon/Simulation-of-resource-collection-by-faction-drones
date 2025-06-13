using System.Collections.Generic;
using FluidBehaviourTreeDesigner.Tasks;
using UnityEditor;
using UnityEngine;

namespace FluidBehaviourTreeDesigner.TreeEditorWindow {
	public enum Mode { NodeAction, CanvasAction, DragNode, PanCanvas, ConnectParent, ConnectChild, InvokeMenu, None }

	public class View 
	{
		private GridRenderer _gridRenderer;
		private Rect _canvas;
		private Vector2 _scrollPoint = Vector2.zero;
		private NodeRenderer _nodeRenderer;

		// public UnityEditor.Editor NodeInspector { set; get; }

		private readonly FluidBehaviourTreeEditorWindow _editorWindow;
		
		private Mode _currentMode = Mode.None;
		private ATaskScriptable _contextNode;
		private Vector2 _initialMousePosition = Vector2.zero;

		private Vector2 _nodeActionOffset = Vector2.zero;

		public View(FluidBehaviourTreeEditorWindow owner) {
			_editorWindow = owner;
			_canvas = new Rect(0, 0, owner.position.width, owner.position.height);
		}

		private void DrawNodes(List<ATaskScriptable> nodes) {
			if (_nodeRenderer == null) _nodeRenderer = new NodeRenderer();

			var count = nodes.Count;
			for (var i = 0; i < count; i++) {
				if (nodes[i] != null) {
					_nodeRenderer.Draw (nodes[i], nodes[i] == FluidBehaviourTreeEditorManager.Manager.SelectedNode);
				}
			}
		}

		public bool Draw(Rect position)
		{
			FluidBehaviourTreeEditorManager.Manager.editorWindow.titleContent =
				new GUIContent(FluidBehaviourTreeEditorManager.Manager.BehaviorTree.Name);
			
			var needsRepaint = HandleMouseEvents(position, FluidBehaviourTreeEditorManager.Manager.BehaviorTree.nodes);

			_scrollPoint = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), _scrollPoint, _canvas);
			
			if (_gridRenderer == null) _gridRenderer = new GridRenderer();
			_gridRenderer.Draw(_scrollPoint, _canvas);

			DrawNodes(FluidBehaviourTreeEditorManager.Manager.BehaviorTree.nodes);
			if (_currentMode == Mode.ConnectChild || _currentMode == Mode.ConnectParent) {
				DrawConnectionLine();
				needsRepaint = true;
			}

			GUI.EndScrollView();

			return needsRepaint;
		}

		private void DrawConnectionLine() {

			var startPos = Vector3.zero;
			var startTan = Vector3.zero;
			var endPos = new Vector3(Event.current.mousePosition.x, Event.current.mousePosition.y, 0);
			var endTan = Vector3.zero;

			if (_currentMode == Mode.ConnectParent) {
				startPos = new Vector3(_contextNode.editorPosition.x + (NodeRenderer.Width / 2), _contextNode.editorPosition.y, 0);
				startTan = startPos + Vector3.down * GridRenderer.step.x * 2;
				endTan = endPos + Vector3.up * GridRenderer.step.x * 2;
			}
			else if(_currentMode == Mode.ConnectChild) {
				startPos = new Vector3(_contextNode.editorPosition.x + (NodeRenderer.Width / 2), _contextNode.editorPosition.y + NodeRenderer.Height, 0);
				startTan = startPos + Vector3.up * GridRenderer.step.x * 2;
				endTan = endPos + Vector3.down * GridRenderer.step.x * 2;
			}

			Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.white, null, 4);
		}

		// Returns true if needs a repaint
		bool HandleMouseEvents(Rect position, List<ATaskScriptable> nodes) {

			// MouseDown //

			// Identify the control being clicked
			if (Event.current.type == EventType.MouseDown) {
				
				// Do nothing for MouseDown on the horizontal scrollbar, if present
				if (_canvas.width > position.width && Event.current.mousePosition.y >= position.height - 20) {
					_currentMode = Mode.None;
				}
				
				// Do nothing for MouseDown on the vertical scrollbar, if present
				else if (_canvas.height > position.height && Event.current.mousePosition.x >= position.width - 20) {
					_currentMode = Mode.None;
				}

				// MouseDown in the canvas, check if in a node or on background
				else {

					// Store the mouse position
					_initialMousePosition = Event.current.mousePosition;

					// Loop through nodes and check if their rects contain the mouse position
					foreach (var t in nodes)
					{
						if (t != null && _nodeRenderer.RectForNode(t, _scrollPoint).Contains (Event.current.mousePosition))
						{

							// Connect a parent to a child
							if (_currentMode == Mode.ConnectChild) {
								FluidBehaviourTreeEditorManager.Manager.Connect (_contextNode, t);
								_editorWindow.wantsMouseMove = false;
								_currentMode = Mode.None;
								break;
							}

							// Connect a child to a parent
							if (_currentMode == Mode.ConnectParent) {
								FluidBehaviourTreeEditorManager.Manager.Connect (t, _contextNode);
								_editorWindow.wantsMouseMove = false;
								_currentMode = Mode.None;
								break;
							}

							// Perform a node action at key up
							_currentMode = Mode.NodeAction;
							_contextNode = t;
							_nodeActionOffset = Event.current.mousePosition - t.editorPosition;

						}
					}

					// Cancel the connection
					if (_currentMode == Mode.ConnectParent || _currentMode == Mode.ConnectChild) {
						_editorWindow.wantsMouseMove = false;
						_currentMode = Mode.None;
					}

					// MouseDown on the canvas background enables panning the view
					if (_currentMode == Mode.None) {
						_currentMode = Mode.CanvasAction;
					}
				}
			}

			// Mouse Up //

			// MouseUp resets the current interaction mode to None
			if (Event.current.type == EventType.MouseUp) {

				// Select node
				if (_currentMode == Mode.NodeAction && Event.current.button == 0) {
					_currentMode = Mode.None;
					FluidBehaviourTreeEditorManager.Manager.SelectNode (_contextNode);
					return true;
				}

				// Deselect node
				if (_currentMode == Mode.CanvasAction && Event.current.button == 0) {
					FluidBehaviourTreeEditorManager.Manager.SelectNode (null);
					_currentMode = Mode.None;
					return true;
				}

				// Context Menu
				if (Event.current.button == 1) {

					if (_currentMode == Mode.NodeAction) {
						_editorWindow.ShowContextMenu(Event.current.mousePosition, _contextNode);
					} else if (_currentMode == Mode.CanvasAction) {
						_editorWindow.ShowContextMenu(Event.current.mousePosition, null);
					}

					_currentMode = Mode.None;
				}

				// Resize canvas after a drag
				else if (_currentMode == Mode.DragNode) {
					ResizeCanvas();
					_currentMode = Mode.None;
					return true;
				}

				else {
					_currentMode = Mode.None;
				}

			}

			// Mouse Drag //

			if (Event.current.type == EventType.MouseDrag && Event.current.button == 0) {

				// Switch to Pan mode
				if (_currentMode == Mode.CanvasAction) {
					_currentMode = Mode.PanCanvas;
				}

				// Switch to node dragging mode
				if (_currentMode == Mode.NodeAction && _contextNode != null) {

					float deltaX = Mathf.Abs (Event.current.mousePosition.x - _initialMousePosition.x);
					float deltaY = Mathf.Abs (Event.current.mousePosition.y - _initialMousePosition.y);

					// Ignore mouse drags inside nodes lesser than the grid step. These would be rounded,
					// and make selecting a node slightly more difficult.
					if (deltaX >= GridRenderer.step.x || deltaY >= GridRenderer.step.y) {
						_currentMode = Mode.DragNode;
					}
				}

				// Pan if the mouse drag initiated by MouseDown outside any windows
				if (_currentMode == Mode.PanCanvas) {
					_scrollPoint.x += - Event.current.delta.x;
					_scrollPoint.y += - Event.current.delta.y;
					_currentMode = Mode.PanCanvas;
					return true;
				}

				// Drag a node
				if (_currentMode == Mode.DragNode) {
					Vector2 newPositionAbs = Event.current.mousePosition - _nodeActionOffset;
					float x = newPositionAbs.x - (newPositionAbs.x % GridRenderer.step.x);
					float y = newPositionAbs.y - (newPositionAbs.y % GridRenderer.step.y);
					DragNode (_contextNode, new Vector2(x, y));
					_currentMode = Mode.DragNode;
					return true;
				}
			}

			return false;
		}

		public void ResizeCanvas() {
			var newCanvas = new Rect(0, 0, _editorWindow.position.width, _editorWindow.position.height);
			foreach (var node in FluidBehaviourTreeEditorManager.Manager.BehaviorTree.nodes) {
				var xOffset = node.editorPosition.x + NodeRenderer.Width + GridRenderer.step.x * 2;
				if (xOffset > newCanvas.width) {
					newCanvas.width = xOffset;
				}
				var yOffset = node.editorPosition.y + NodeRenderer.Height + GridRenderer.step.y * 2;
				if (yOffset > newCanvas.height) {
					newCanvas.height = yOffset;
				}
				_canvas = newCanvas;
			}
		}

		private void DragNode(ATaskScriptable node, Vector2 newPosition) {

			if (Application.isPlaying) {
				return;
			}

			if (node.ChildCount > 0 && !Event.current.shift) {
				for (var i = 0; i < node.ChildCount; i++) {
					var childOffset = node.Children[i].editorPosition - node.editorPosition;
					var newChildPosition = newPosition + childOffset;
					
					DragNode (node.Children[i], newChildPosition);
				}
			}

			FluidBehaviourTreeEditorManager.Manager.SetEditorPosition(node, newPosition);
		}

		public void ConnectParent(ATaskScriptable node) {
			_editorWindow.wantsMouseMove = true;
			_contextNode = node;
			_currentMode = Mode.ConnectParent;
		}

		public void ConnectChild(ATaskScriptable node) {
			_editorWindow.wantsMouseMove = true;
			_contextNode = node;
			_currentMode = Mode.ConnectChild;
		}

	}

}