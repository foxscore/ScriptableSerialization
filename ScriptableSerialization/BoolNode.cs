namespace ScriptableSerialization
{
    public class BoolNode : BaseNode
    {
        public bool Value;
        
        public BoolNode(bool value)
        {
            Value = value;
        }

        public override string Serialize()
        {
            return Value ? "true" : "false";
        }
        
        public static implicit operator BoolNode(bool value) => new BoolNode(value);
    }
}