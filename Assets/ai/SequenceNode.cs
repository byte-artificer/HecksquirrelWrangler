using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ai
{
    public class SequenceNode : BehaviorTreeNode
    {
        public SequenceNode(List<BehaviorTreeNode> children) : base(children)
        {
        }

        public override eNodeState Evaluate()
        {
            bool hasRunningChild = false;
            foreach (var child in children)
            {
                var result = child.Evaluate();

                if (result == eNodeState.FAILURE)
                {
                    State = eNodeState.FAILURE;
                    return State;
                }

                if (result == eNodeState.RUNNING)
                    hasRunningChild = true;
            }

            State = hasRunningChild ? eNodeState.RUNNING : eNodeState.SUCCESS;
            return State;
        }
    }
}
