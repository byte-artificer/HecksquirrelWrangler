using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ai
{
    public class SelectorNode : BehaviorTreeNode
    {
        public override eNodeState Evaluate()
        {
            foreach (var child in children)
            {
                var result = child.Evaluate();

                if (result == eNodeState.FAILURE)
                    continue;
                else
                {
                    State = result;
                    return State;
                }
            }

            State = eNodeState.FAILURE;
            return State;
        }
    }
}
