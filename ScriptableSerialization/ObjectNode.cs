using System.Diagnostics.CodeAnalysis;

namespace ScriptableSerialization
{
    public class ObjectNode : BaseNode
    {
        private List<Tuple<string, BaseNode>> _children = new List<Tuple<string, BaseNode>>();

        public BaseNode this[string index]
        {
            get
            {
                foreach (var child in _children.Where(child => child.Item1 == index))
                    return child.Item2;
                return new NullNode();
            }
            set
            {
                var i = _children.FindIndex(n => n.Item1 == index);
                if (i == -1)
                    _children.Add(new Tuple<string, BaseNode>(index, value));
                else
                    _children[i] = new Tuple<string, BaseNode>(index, value);
            }
        }

        public int Length => _children.Count;

        public bool HasKey(string key) => _children.FindIndex(n => n.Item1 == key) != -1;
        
        public override string Serialize()
        {
            if (_children.Count == 0)
                return "{ }";
            
            var str = "{\n";

            for (var i = 0; i < _children.Count - 1; i++)
                str += $"{MakeIndent()}\"{_children[i].Item1}\": {_children[i].Item2.Serialize()},\n";
            str += $"{MakeIndent()}\"{_children.Last().Item1}\": {_children.Last().Item2.Serialize()}\n";

            str += MakeIndent(-1) + "}";

            return str;
        }

        public void AddField([NotNull] string name, IScriptableSerializable obj) =>
            AddField(name, obj is null ? new NullNode() : obj.GetSerializationNode());
        public void AddField([NotNull] string name, string[] value) => AddField(name, (ArrayNode)value);
        public void AddField([NotNull] string name, float[] value) => AddField(name, (ArrayNode)value);
        public void AddField([NotNull] string name, bool[] value) => AddField(name, (ArrayNode)value);
        public void AddField([NotNull] string name, int[] value) => AddField(name, (ArrayNode)value);
        public void AddField([NotNull] string name, string value) => AddField(name, (StringNode)value);
        public void AddField([NotNull] string name, float value) => AddField(name, (FloatNode)value);
        public void AddField([NotNull] string name, bool value) => AddField(name, (BoolNode)value);
        public void AddField([NotNull] string name, int value) => AddField(name, (IntNode)value);
        public void AddField([NotNull] string name, BaseNode child)
        {
            if (child is null)
            {
                AddField(name, new NullNode());
                return;
            }
            
            IncrementIndentRecursive(this, child);
            _children.Add(new Tuple<string, BaseNode>(name, child));
        }

        private void IncrementIndentRecursive(BaseNode parent, BaseNode node)
        {
            node.Indent = parent.Indent + 1;

            if (node is ObjectNode objNode)
                foreach (var child in objNode._children)
                    IncrementIndentRecursive(objNode, child.Item2);
            else if (node is ArrayNode arrayNode)
                for (var i = 0; i < arrayNode.Length; i++)
                    IncrementIndentRecursive(arrayNode, arrayNode[i]);
        }
    }
}