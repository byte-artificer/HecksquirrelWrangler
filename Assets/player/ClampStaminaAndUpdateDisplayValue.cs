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
    class ClampStaminaAndUpdateDisplayValue : BehaviorTreeNode<PlayerState>
    {
        public ClampStaminaAndUpdateDisplayValue(Transform transform, PlayerState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            _state.Stamina = Mathf.Clamp(_state.Stamina, 0, _state.MaxStamina);

            _state.PlayerStamina.Value = _state.Stamina / _state.MaxStamina;

            RunState = eNodeState.SUCCESS;
            return RunState;
        }
    }
}
