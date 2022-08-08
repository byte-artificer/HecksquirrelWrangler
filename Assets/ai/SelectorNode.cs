using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ai
{
    public class SelectorNode<T> : BehaviorTreeNode<T> where T : BaseEntityState
    {
        public SelectorNode(T state) : base(null, state) { }

        public SelectorNode(T state, params BehaviorTreeNode<T>[] children) : base(null, state, children) { }
        public override eNodeState Evaluate()
        {
            foreach (var child in _children)
            {
                var result = child.Evaluate();

                if (result == eNodeState.FAILURE)
                    continue;
                else
                {
                    RunState = result;
                    return RunState;
                }
            }

            RunState = eNodeState.FAILURE;
            return RunState;
        }
    }
}
