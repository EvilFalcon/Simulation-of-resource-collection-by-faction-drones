using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Ecs.Commands.Generator.Editor.Utils.ScriptsParser
{
    public static class ScriptsTraveler
    {
        private const string SEARCH_PATTERN = "*.cs";

        public static void Run(string directoryPath, IScriptHandler handler)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
            }

            var csFiles = Directory.GetFiles(directoryPath, SEARCH_PATTERN, SearchOption.AllDirectories);

            foreach (var csFile in csFiles)
            {
                var fileContent = File.ReadAllText(csFile);

                try
                {
                    var tree = CSharpSyntaxTree.ParseText(fileContent);
                    var root = tree.GetRoot();

                    handler.HandleSyntaxRoot(root);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error processing file {csFile}: {ex.Message}", ex);
                }
            }
        }
    }

    public interface IScriptHandler
    {
        void HandleSyntaxRoot(SyntaxNode syntaxNode);
    }
}