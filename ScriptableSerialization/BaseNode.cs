namespace ScriptableSerialization
{
    public abstract class BaseNode
    {
        public int Indent { get; set; } = 1;
        
        public abstract string Serialize();

        protected string MakeIndent(int modifier = 0)
        {
            var str = "";
            for (var i = 0; i < Indent + modifier; i++)
            {
                str += "    ";
            }
            return str;
        }

        public override string ToString() => Serialize();
    }
}