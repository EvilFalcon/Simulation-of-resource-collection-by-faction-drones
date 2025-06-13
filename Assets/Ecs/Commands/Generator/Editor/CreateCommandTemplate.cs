using System;
using System.Collections.Generic;
using System.Linq;
using Ecs.Commands.Generator.Editor.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecs.Commands.Generator.Editor
{
    public static class CreateCommandTemplate
    {
        public static MethodAndUsingInfo GenerateTemplate(Type commandType)
        {
            var methodName = $"{commandType.Name}".Replace("Cmd", "").Replace("Command", "");
            var method = SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                    SyntaxFactory.Identifier(methodName))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword));

            var parameters = new List<ParameterSyntax>
            {
                SyntaxFactory.Parameter(SyntaxFactory.Identifier("commandBuffer"))
                    .WithType(SyntaxFactory.ParseTypeName("ICommandBuffer"))
                    .WithModifiers(SyntaxTokenList.Create(SyntaxFactory.Token(SyntaxKind.ThisKeyword)))
            };

            var fields = commandType.GetFields();
            var usingDirectives = new List<UsingDirectiveSyntax>
            {
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(commandType.Namespace))
                    .NormalizeWhitespace()
            };

            foreach (var field in fields)
            {
                var usingDirective = SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(field.FieldType.Namespace))
                    .NormalizeWhitespace();
                usingDirectives.Add(usingDirective);

                var nullable = Nullable.GetUnderlyingType(field.FieldType) != null;
                var typeName = field.FieldType.Name;

                if (typeName == "Action")
                {
                    typeName = "System.Action";
                }

                if (nullable)
                {
                    var underlyingType = Nullable.GetUnderlyingType(field.FieldType);
                    var csharpType = underlyingType.Name.FromDotNetTypeToCSharpType();
                    typeName = $"{csharpType}?";
                }

                if (field.FieldType.IsGenericType)
                {
                    var genericType = field.FieldType.GetGenericTypeDefinition();
                    var args = field.FieldType.GetGenericArguments();
                    var mainType = genericType.Name.Split("`")[0];
                    typeName = mainType;

                    if (mainType == "Action")
                    {
                        typeName = "System.Action";
                    }

                    if (args.Length > 0)
                    {
                        typeName += "<";

                        for (var i = 0; i < args.Length - 1; i++)
                        {
                            var arg = args[i];
                            typeName += $"{arg.Name},";
                        }

                        typeName += $"{args.Last()}";
                        typeName += ">";
                    }
                }

                var parameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier($"{field.Name.FirstCharToLower()}"))
                    .WithType(SyntaxFactory.ParseTypeName(typeName));

                if (nullable)
                {
                    parameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier($"{(field.Name.FirstCharToLower())}"))
                        .WithType(SyntaxFactory.ParseTypeName(typeName));
                }

                parameters.Add(parameter);
            }

            method = method.AddParameterListParameters(parameters.ToArray());

            //ref does not work, so refs are added after code generation 
            var statements = new List<StatementSyntax>();
            var st = SyntaxFactory.ParseStatement($"var FLAG_CODE_GEN_command = FLAG_CODE_GEN_commandBuffer.Create<{commandType.Name}>();");
            statements.Add(st);

            foreach (var field in fields)
            {
                var statement = SyntaxFactory.ParseStatement($"command.{field.Name} = {field.Name.FirstCharToLower()};");
                statements.Add(statement);
            }

            var block = SyntaxFactory.Block();
            block = block.AddStatements(statements.ToArray());
            method = method.WithBody(block);

            return new MethodAndUsingInfo(method, usingDirectives);
        }
    }
}