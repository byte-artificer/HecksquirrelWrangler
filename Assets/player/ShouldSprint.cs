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
    public class ShouldSprint : BehaviorTreeNode<PlayerState>
    {
        public ShouldSprint(Transform transform, PlayerState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            var wasSprinting = _state.Sprinting;
            var canSprint = _state.SprintHeld && (wasSprinting ? _state.Stamina > 0f : _state.PlayerStamina.Value > 0.15f); //can continue sprinting to zero, but can't start sprinting with less than 15%
            _state.Sprinting = canSprint;
            RunState = canSprint ? eNodeState.SUCCESS : eNodeState.FAILURE;
            return RunState;
        }
    }
}
