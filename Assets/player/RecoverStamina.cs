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
    public class RecoverStamina : BehaviorTreeNode<PlayerState>
    {
        public RecoverStamina(Transform transform, PlayerState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            _state.Stamina += 0.85f * Time.deltaTime;
            RunState = eNodeState.SUCCESS;
            return RunState;
        }

    }
}
