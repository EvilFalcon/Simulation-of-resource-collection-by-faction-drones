using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecs.Commands.Generator.Editor.Utils
{
    public class UsingDirectivesHashSet : HashSet<UsingDirectiveSyntax>
    {
        private readonly HashSet<string> _values = new();

        public new bool Add(UsingDirectiveSyntax usingDirectiveSyntax)
        {
            var str = usingDirectiveSyntax.ToString();
            var canAdd = _values.Add(str);

            return canAdd && base.Add(usingDirectiveSyntax);
        }

        public void AddRange(IEnumerable<UsingDirectiveSyntax> directives)
        {
            foreach (var directive in directives)
            {
                Add(directive);
            }
        }
    }
}