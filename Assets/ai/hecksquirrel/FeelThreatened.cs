using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai.hecksquirrel
{
    public class FeelThreatened : BehaviorTreeNode<HeckSquirrelState>
    {
        readonly float _threatDistance;
        public FeelThreatened(Transform transform, HeckSquirrelState state, float threatDistance) : base(transform, state) { _threatDistance = threatDistance; }

        public override eNodeState Evaluate()
        {
            var distance = Vector3.Distance(_state.Player.transform.position, _transform.position);

            if (distance <= _threatDistance)
            {
                _state.IsFleeing = true;

                Debug.Log($"Feels threatened {_threatDistance}");

                RunState = eNodeState.SUCCESS;
                return RunState;
            }

            RunState = eNodeState.FAILURE;
            return RunState;
        }
    }
}
