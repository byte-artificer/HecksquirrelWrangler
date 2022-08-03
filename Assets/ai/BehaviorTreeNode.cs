using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ai
{
    public enum eNodeState { RUNNING, SUCCESS, FAILURE }
    public class BehaviorTreeNode
    {
        protected eNodeState State;
        public BehaviorTreeNode Parent;
        protected List<BehaviorTreeNode> children = new List<BehaviorTreeNode>();
        Dictionary<string, object> _context = new Dictionary<string, object>();

        public BehaviorTreeNode() { Parent = null; }
        public BehaviorTreeNode(List<BehaviorTreeNode> children) { foreach (var c in children) Add(c); }

        public virtual eNodeState Evaluate() => eNodeState.FAILURE;

        public void Add(BehaviorTreeNode child)
        {
            child.Parent = this;
            children.Add(child);
        }

        public void SetData(string key, object value)
        {
            _context[key] = value;
        }

        public object GetData(string key)
        {
            object val = _context.GetValueOrDefault(key, Parent?.GetData(key));
            return val;
        }

        public bool ClearData(string key)
        {
            var removed = Parent?.ClearData(key) ?? false;
            return removed || _context.Remove(key);
        }
    }
}
