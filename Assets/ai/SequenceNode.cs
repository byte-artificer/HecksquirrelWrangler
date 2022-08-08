using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ai
{
    public class SequenceNode<T> : BehaviorTreeNode<T> where T : BaseEntityState
    {
        public SequenceNode(T state, params BehaviorTreeNode<T>[] children) : base(null, state, children) { }

        public override eNodeState Evaluate()
        {
            bool hasRunningChild = false;
            foreach (var child in _children)
            {
                var result = child.Evaluate();

                if (result == eNodeState.FAILURE)
                {
                    RunState = eNodeState.FAILURE;
                    return RunState;
                }

                if (result == eNodeState.RUNNING)
                    hasRunningChild = true;
            }

            RunState = hasRunningChild ? eNodeState.RUNNING : eNodeState.SUCCESS;
            return RunState;
        }
    }
}
