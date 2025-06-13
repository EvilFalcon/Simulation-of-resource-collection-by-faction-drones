using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecs.Commands.Generator.Editor.Utils.ScriptsParser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecs.Commands.Generator.Editor.Utils.ScriptHandler
{
    public class CommandsScriptsHandler : IScriptHandler
    {
        private readonly string _namespace;

        private readonly HashSet<string> _rawUsings = new();
        private readonly StringBuilder _usingsBuilder = new();
        private readonly StringBuilder _extensionsBuilder = new();

        public CommandsScriptsHandler(string ns)
        {
            _namespace = ns;
        }

        public void HandleSyntaxRoot(SyntaxNode root)
        {
            var structs = root.DescendantNodes()
                .OfType<StructDeclarationSyntax>()
                .Where(HasCommandAttribute)
                .ToList();
            
            if (structs.Count <= 0)
                return;
            
            var rawStringUsings = root.DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .Select(u => u.Name!.ToString())
                .ToArray();
            
            var stringNamespacesAsRawUsings = root.DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>()
                .Select(u => u.Name!.ToString())
                .ToArray();
            
            foreach (var rawStringUsing in rawStringUsings)
                TryAddStringUsing(rawStringUsing);
            
            foreach (var rawStringUsing in stringNamespacesAsRawUsings)
                TryAddStringUsing(rawStringUsing);
            
            foreach (var structDeclarationSyntax in structs)
            {
                var newExtension = GenerateExtensionMethod(structDeclarationSyntax);
                _extensionsBuilder.AppendLine(newExtension);
            }
        }

        public void GetCommandsExtensionsCode(out string fileCode)
        {
            TryAddStringUsing("JCMG.EntitasRedux.Commands");
            fileCode = "";
            fileCode = GenerateFileContents(_usingsBuilder.ToString(), _extensionsBuilder.ToString(), _namespace);
        }

        private void TryAddStringUsing(string rawNewUsing)
        {
            if (_rawUsings.Contains(rawNewUsing))
                return;

            _rawUsings.Add(rawNewUsing);
            _usingsBuilder.AppendLine($"using {rawNewUsing};");
        }

        private static bool HasCommandAttribute(BaseTypeDeclarationSyntax node)
        {
            return node.AttributeLists
                .SelectMany(al => al.Attributes)
                .Any(a => a.Name.ToString() == "Command");
        }

        private static string GenerateExtensionMethod(StructDeclarationSyntax @struct)
        {
            var structName = @struct.Identifier.Text;
            var fields = @struct.Members
                .OfType<FieldDeclarationSyntax>()
                .Where(IsPublic)
                .ToList();

            var parameters = fields.Select(field =>
                    $"{field.Declaration.Type} {field.Declaration.Variables.First().Identifier.Text.FirstCharToLower()}"
                )
                .ToList();

            parameters.Insert(0, "this ICommandBuffer commandBuffer");

            var argumentsString = new StringBuilder();

            for (var index = 0; index < parameters.Count; index++)
            {
                var parameter = parameters[index];
                argumentsString.Append(parameter);
                if (index < parameters.Count - 1)
                    argumentsString.Append(", ");
            }

            var assignments = fields.Select(field =>
                    $"            command.{field.Declaration.Variables.First().Identifier.Text} = {field.Declaration.Variables.First().Identifier.Text.FirstCharToLower()};"
                )
                .ToList();

            assignments.Insert(0, $"            ref var command = ref commandBuffer.Create<{structName}>();");
            var assignmentsString = new StringBuilder();

            for (var index = 0; index < assignments.Count; index++)
            {
                var assignment = assignments[index];
                assignmentsString.Append(assignment);
                if (index < parameters.Count - 1)
                    assignmentsString.Append("\n");
            }

            return $@"
        public static void {structName.Replace("Command", "")}({argumentsString})
        {{
{assignmentsString}
        }}";
        }

        private static bool IsPublic(FieldDeclarationSyntax field)
        {
            return field.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword);
        }

        private static string GenerateFileContents(string usingDirectives, string extensions, string namespaceName)
        {
            return $@"//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Commands Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
{usingDirectives}

namespace {namespaceName}
{{
    public static partial class CommandBufferExtensions
    {{
{extensions}
    }}
}}
";
        }
    }
}