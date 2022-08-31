namespace ScriptableSerialization
{
    public class IntNode : BaseNode
    {
        public int Value { get; set; }
        
        public IntNode(int value)
        {
            Value = value;
        }
        
        public override string Serialize()
        {
            return Value.ToString();
        }

        public static implicit operator IntNode(int i) => new IntNode(i);
    }
}