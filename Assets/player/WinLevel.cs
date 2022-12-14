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
    public class WinLevel : BehaviorTreeNode<PlayerState>
    {
        public WinLevel(Transform transform, PlayerState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            if (_state.HeckSquirrelStates.All(x => x.WarpedOut))
            {
                _state.MovementInput = null;

                _state.WinLevel = true;

                RunState = eNodeState.SUCCESS;
            }
            else
                RunState = eNodeState.FAILURE;

            return RunState;
        }
    }
}
