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
    public class DoMovement : BehaviorTreeNode<PlayerState>
    {
        public DoMovement(Transform transform, PlayerState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            if (_state.MovementInput.HasValue)
            {
                _state.MoveVal = _state.MovementInput.Value;
                RunState = eNodeState.RUNNING;
            }
            else
            {
                _state.MoveVal = Vector2.zero;
                RunState = eNodeState.FAILURE;
            }

            return RunState;
        }
    }
}
