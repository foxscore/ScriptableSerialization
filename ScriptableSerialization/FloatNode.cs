using System.Globalization;

namespace ScriptableSerialization
{
    public class FloatNode : BaseNode
    {
        public float Value;
        
        public FloatNode(float value)
        {
            Value = value;
        }
        
        public override string Serialize() => Value.ToString(CultureInfo.InvariantCulture);
        
        public static implicit operator FloatNode(float value) => new FloatNode(value);
    }
}