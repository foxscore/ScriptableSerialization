using System.Collections.Generic;
using System.Linq;

namespace ScriptableSerialization
{
    public class ArrayNode : BaseNode
    {
        private List<BaseNode> _nodes = new List<BaseNode>();
        
        public BaseNode this[int index]
        {
            get => _nodes[index];
            set => _nodes[index] = value;
        }
        
        public int Length => _nodes.Count;
        
        public override string Serialize()
        {
            if (_nodes.Count == 0)
                return "[ ]";
            
            var str = "[\n";

            for (var i = 0; i < _nodes.Count - 1; i++)
                str += $"{MakeIndent()}{_nodes[i].Serialize()},\n";
            str += $"{MakeIndent()}{_nodes.Last().Serialize()}\n";
            
            str += MakeIndent(-1) + "]";
            
            return str;
        }
        
        public void Add(string value) => _nodes.Add((StringNode)value);
        public void Add(int value) => _nodes.Add((IntNode)value);
        public void Add(float value) => _nodes.Add((FloatNode)value);
        public void Add(bool value) => _nodes.Add((BoolNode)value);
        public void Add(BaseNode node) =>  _nodes.Add(node ?? new NullNode());
        
        public static implicit operator ArrayNode(string[] array)
        {
            if (array is null)
                return new ArrayNode();
            
            var node = new ArrayNode();
            foreach (var item in array)
                node.Add(item);
            return node;
        }
        
        public static implicit operator ArrayNode(int[] array)
        {
            if (array is null)
                return new ArrayNode();
            
            var node = new ArrayNode();
            foreach (var item in array)
                node.Add(item);
            return node;
        }
        
        public static implicit operator ArrayNode(float[] array)
        {
            if (array is null)
                return new ArrayNode();
            
            var node = new ArrayNode();
            foreach (var item in array)
                node.Add(item);
            return node;
        }
        
        public static implicit operator ArrayNode(bool[] array)
        {
            if (array is null)
                return new ArrayNode();
            
            var node = new ArrayNode();
            foreach (var item in array)
                node.Add(item);
            return node;
        }
    }
}