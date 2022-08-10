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
    public class SprintAndExpendStamina : BehaviorTreeNode<PlayerState>
    {
        public SprintAndExpendStamina(Transform transform, PlayerState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            _state.Stamina -= Time.deltaTime;
            _state.CurrentSpeed = _state.BaseSpeed * 1.95f;

            RunState = eNodeState.RUNNING;
            return RunState;
        }
    }
}
