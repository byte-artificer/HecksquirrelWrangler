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
    public class ShouldRecoverStamina : BehaviorTreeNode<PlayerState>
    {
        public ShouldRecoverStamina(Transform transform, PlayerState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            var canRecover = !_state.SprintHeld && _state.PlayerStamina.Value < 1;
            RunState = canRecover ? eNodeState.SUCCESS : eNodeState.FAILURE;
            return RunState;
        }
    }
}
