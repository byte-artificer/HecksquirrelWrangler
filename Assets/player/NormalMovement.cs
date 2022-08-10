using Assets.ai;
using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.player
{
    public class NormalMovement : BehaviorTreeNode<PlayerState>
    {
        public NormalMovement(Transform transform, PlayerState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            _state.CurrentSpeed = _state.BaseSpeed;
            RunState = eNodeState.SUCCESS;
            return RunState;
        }
    }
}
