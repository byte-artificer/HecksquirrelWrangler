using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai.hecksquirrel
{
    public class FeelSafe : BehaviorTreeNode<HeckSquirrelState>
    {
        public FeelSafe(Transform transform, HeckSquirrelState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            Debug.Log("Feels safe");
            _state.CurrentSpeed = _state.BaseSpeed;
            _state.IsFleeing = false;

            RunState = eNodeState.SUCCESS;
            return RunState;
        }
    }
}
