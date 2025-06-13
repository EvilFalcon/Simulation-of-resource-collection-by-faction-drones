using FluidBehaviourTreeDesigner.Tasks;
using UnityEditor;
using UnityEngine;

namespace FluidBehaviourTreeDesigner.TreeEditorWindow {



	public class FluidBehaviourTreeEditorWindow : EditorWindow 
	{
		private View _view;
		private GUIStyle _noSelectionStyle;
		private bool _debugMode;

		[MenuItem("Tools/Behavior Tree Editor")]
		public static void ShowWindow() {
			var editor = GetWindow<FluidBehaviourTreeEditorWindow>();
			editor.minSize = new Vector2(100, 100);
			editor.titleContent = new GUIContent("Behavior Tree");
		}

		private void OnSelectionChange() {
			Repaint ();
		}

		private void OnEnable() {
			_noSelectionStyle = new GUIStyle {fontSize = 24, alignment = TextAnchor.MiddleCenter};
			if (FluidBehaviourTreeEditorManager.Manager != null) FluidBehaviourTreeEditorManager.Manager.editorWindow = this;
		}

		private void OnDisable() {
			if (FluidBehaviourTreeEditorManager.Manager) FluidBehaviourTreeEditorManager.Manager.editorWindow = null;
		}

		private void OnDestroy() {
			if (FluidBehaviourTreeEditorManager.Manager) FluidBehaviourTreeEditorManager.Manager.editorWindow = null;
		}

		void OnGUI() {

			if (FluidBehaviourTreeEditorManager.Manager != null && FluidBehaviourTreeEditorManager.Manager.BehaviorTree != null) {

				FluidBehaviourTreeEditorManager.Manager.editorWindow = this;

				if (_view == null)
					_view = new View(this);

				// if (_view.NodeInspector != null)
				// 	_view.NodeInspector.OnInspectorGUI();

				bool viewNeedsRepaint = _view.Draw(position);
				if (viewNeedsRepaint)
					Repaint ();
				
				_view.ResizeCanvas();

			} else {
				GUI.Label(new Rect(0, 0, position.width, position.height), "No Behavior Tree selected", _noSelectionStyle);
			}

		}

		public void ShowContextMenu(Vector2 point, ATaskScriptable node) {

			if (Application.isPlaying) {
				return;
			}
			
			// var menu = new GenericMenu();
			var contextMenu = new ContextMenu(node, point);
			contextMenu.DisplayAdd(Add);
			contextMenu.DisplaySave(Save);
			contextMenu.AddSeparator();
			contextMenu.DisplayNodeParentActions(Unparent, ConnectParent);
			contextMenu.AddSeparator();
			contextMenu.DisplayNodeConnectChildAction(ConnectChild);
			contextMenu.AddSeparator();
			contextMenu.DisplayNodeDeleteAction(Delete);
			contextMenu.DisplayDropDown();
		}

		// Context Menu actions

		private void Add(object userData) {
			if (userData is MenuAction menuAction)
			{
				FluidBehaviourTreeEditorManager.Manager.Add(menuAction.Node, menuAction.Position, menuAction.TaskType.Type);	
			}
			_view.ResizeCanvas();
			Repaint ();
		}

		private void Unparent(object userData) {
			if (userData is MenuAction menuAction)
			{
				FluidBehaviourTreeEditorManager.Manager.Unparent(menuAction.Node);
			}
			Repaint ();
		}

		private void ConnectParent(object userData)
		{
			if (userData is MenuAction menuAction)
			{
				_view.ConnectParent(menuAction.Node);
			}
		}

		private void ConnectChild(object userData)
		{
			if (userData is MenuAction menuAction)
			{
				_view.ConnectChild(menuAction.Node);
			}
		}

		private void Delete(object userData) {
			if (userData is MenuAction menuAction)
			{
				FluidBehaviourTreeEditorManager.Manager.Delete(menuAction.Node);
			}
			Repaint();
		}

		private void Save(object userData) {
			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
			Debug.Log ("Save");
		}
	}

	
}