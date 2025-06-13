using System.IO;
using Ecs.Commands.Generator.Editor.Utils.ScriptHandler;
using Ecs.Commands.Generator.Editor.Utils.ScriptsParser;
using UnityEditor;
using UnityEngine;

namespace Ecs.Commands.Generator.Editor
{
    public class CommandsGeneratorWindow : EditorWindow
    {
        private const string DEFAULT_PATH = "Generated/Commands";
        private const string GENERATION_PATH_KEY = "CommandsGenerationPath";
        private const string GENERATED_EXTENSIONS_CLASS_NAME = "CommandBufferExtensions";

        private static string _generationPathKey;
        private static string _defaultPath;
        private static string _generationPath;

        private static void InitGenerators()
        {
            _generationPathKey = GENERATION_PATH_KEY;
            _defaultPath = DEFAULT_PATH;
        }

        [MenuItem("Tools/Commands/Settings")]
        public static void MiOpenWindow()
        {
            var window = GetWindowWithRect<CommandsGeneratorWindow>(new Rect(0, 0, 300, 100), false, "Commands generator");
            window.Show();
        }

        [MenuItem("Tools/Commands/Generate")]
        public static void GenerateProperties()
        {
            InitGenerators();
            _generationPath = EditorPrefs.GetString(_generationPathKey, _defaultPath);

            if (string.IsNullOrEmpty(_generationPath))
            {
                Debug.LogError($"[{nameof(CommandsGeneratorWindow)}] Generation path can't be empty!");
                return;
            }

            var generatedFileDirectory = new DirectoryInfo(Path.Combine(Application.dataPath, _generationPath));
            var baseNamespace = _generationPath.Replace("/", ".");

            var commandsScriptsHandler = new CommandsScriptsHandler(baseNamespace);
            ScriptsTraveler.Run(Application.dataPath, commandsScriptsHandler);

            commandsScriptsHandler.GetCommandsExtensionsCode(out var generatedData);

            var fileName = $"{GENERATED_EXTENSIONS_CLASS_NAME}.cs";
            var fileINfo = new FileInfo(Path.Combine(generatedFileDirectory.FullName, fileName));

            Save(fileINfo, generatedData);

            AssetDatabase.Refresh();
        }

        private void OnEnable()
        {
            InitGenerators();
            _generationPath = EditorPrefs.GetString(_generationPathKey, _defaultPath);
        }

        private void OnGUI()
        {
            DrawGenerationPath();
            DrawGenerateButton();
        }

        private static void DrawGenerationPath()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("GenerationPath");
            _generationPath = GUILayout.TextField(_generationPath);
            GUILayout.EndHorizontal();
        }

        private static void DrawGenerateButton()
        {
            if (GUILayout.Button("Save path"))
                EditorPrefs.SetString(_generationPathKey, _generationPath);

            if (GUILayout.Button("Generate Commands"))
                GenerateProperties();
        }

        private void OnDisable()
        {
            EditorPrefs.SetString(_generationPathKey, _generationPath);
        }

        private static void Save(FileInfo fileInfo, in string generatedData)
        {
            if (fileInfo.Directory is { Exists: false })
                fileInfo.Directory.Create();
            if (fileInfo.Exists)
                fileInfo.Delete();
            File.WriteAllText(fileInfo.FullName, generatedData);
        }
    }
}