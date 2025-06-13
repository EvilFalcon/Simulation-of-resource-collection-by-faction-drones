using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecs.Commands.Generator.Editor.Utils
{
	public struct MethodAndUsingInfo
	{
		public MethodDeclarationSyntax MethodDeclaration;
		public List<UsingDirectiveSyntax> UsingDirectives;

		public MethodAndUsingInfo(
			MethodDeclarationSyntax declaration,
			List<UsingDirectiveSyntax> usingDirectives
		)
		{
			MethodDeclaration = declaration;
			UsingDirectives = usingDirectives;
		}
	}
}