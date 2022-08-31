namespace ScriptableSerialization
{
    public class NullNode : BaseNode
    {
        public override string Serialize()
        {
            return "null";
        }
    }
}