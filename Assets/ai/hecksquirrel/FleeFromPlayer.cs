using Assets.extensions;
using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.ai.hecksquirrel
{
    public class FleeFromPlayer : BehaviorTreeNode<HeckSquirrelState>
    {
        readonly float _fleeSpeedMultiplier;
        public FleeFromPlayer(Transform transform, HeckSquirrelState state, float fleeSpeedMultiplier) : base(transform, state) { _fleeSpeedMultiplier = fleeSpeedMultiplier; }

        public override eNodeState Evaluate()
        {
            Debug.Log($"Fleeing player {_fleeSpeedMultiplier}");

            _state.CurrentSpeed = _state.BaseSpeed * _fleeSpeedMultiplier;

            if ((Time.time - _state.LastRetargetTime) > 0.1f)
            {
                var movement = _transform.position - _state.Player.transform.position;
                movement = Quaternion.Euler(0, 0, Random.Range(-20, 20)) * movement;

                var target = _transform.position + movement;

                _state.MovementTarget = target.ToVector2();
                _state.DebugTarget.transform.position = target;
                _state.LastRetargetTime = Time.time;
            }
            RunState = eNodeState.SUCCESS;
            return RunState;
        }
    }
}
