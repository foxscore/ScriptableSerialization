using System.Text.RegularExpressions;

namespace ScriptableSerialization
{
    public class StringNode : BaseNode
    {
        public string Value { get; set; }
        
        public StringNode(string value)
        {
            Value = value;
        }
        
        public override string Serialize()
        {
            if (Value is null)
                return "null";

            var str = Value;
            // Replace \ with \\
            str = str.Replace("\\", "\\\\");
            // Replace " with \"
            str = str.Replace("\"", "\\\"");
            // Replace newlines with \n
            str = str.Replace("\n", "\\n");

            return $"\"{str}\"";
        }
        
        public static implicit operator StringNode(string value) => new StringNode(value);
    }
}