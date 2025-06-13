using System;
using System.Reflection;

namespace Ecs.Commands.Generator.Editor.Utils
{
    public static class Extensions
    {
        public static bool HasAttribute<T>(this MemberInfo type)
            where T : Attribute
            => type.GetCustomAttribute<T>() != null;
        
        public static string FirstCharToLower(this string source) =>
            source.Substring(0, 1).ToLower() + source.Substring(1);
        
        public static string FromDotNetTypeToCSharpType(this string dotNetTypeName, bool isNull = false)
        {
            var cstype = "";
            var nullable = isNull ? "?" : "";
            var prefix = "System.";
            var typeName = dotNetTypeName.StartsWith(prefix) ? dotNetTypeName.Remove(0, prefix.Length) : dotNetTypeName;

            switch (typeName)
            {
                case "Boolean":
                    cstype = "bool";
                    break;
                case "Byte":
                    cstype = "byte";
                    break;
                case "SByte":
                    cstype = "sbyte";
                    break;
                case "Char":
                    cstype = "char";
                    break;
                case "Decimal":
                    cstype = "decimal";
                    break;
                case "Double":
                    cstype = "double";
                    break;
                case "Single":
                    cstype = "float";
                    break;
                case "Int32":
                    cstype = "int";
                    break;
                case "UInt32":
                    cstype = "uint";
                    break;
                case "Int64":
                    cstype = "long";
                    break;
                case "UInt64":
                    cstype = "ulong";
                    break;
                case "Object":
                    cstype = "object";
                    break;
                case "Int16":
                    cstype = "short";
                    break;
                case "UInt16":
                    cstype = "ushort";
                    break;
                case "String":
                    cstype = "string";
                    break;

                default:
                    cstype = typeName;
                    break; // do nothing
            }

            return $"{cstype}{nullable}";
        }
    }
    
}