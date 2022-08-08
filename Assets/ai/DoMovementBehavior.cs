using Assets.ai.hecksquirrel;
using Assets.extensions;
using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.ai
{
    public class DoMovementBehavior<T> : BehaviorTreeNode<T> where T : BaseEntityState
    {
        Vector2? _previousTarget;
        float _previousDistance;
        int _atApproximatelySameDistanceCount;

        public DoMovementBehavior(Transform transform, T state) : base(transform, state) { }
        public override eNodeState Evaluate()
        {
            var currentPosition2D = _transform.position.ToVector2();

            if (_state.MovementTarget == null)
            {
                RunState = eNodeState.FAILURE;
                return RunState;
            }

            var distance = Vector2.Distance(currentPosition2D, _state.MovementTarget.Value);

            if (!_state.MovementTarget.Equals(_previousTarget))
            {
                _previousTarget = _state.MovementTarget;
                _previousDistance = distance;
                _atApproximatelySameDistanceCount = 0;
            }

            if (distance <= 0.01f)
            {
                Debug.Log("reached target");

                _state.MovementTarget = null;
                RunState = eNodeState.FAILURE;
                return RunState;
            }

            if (Math.Abs(distance - _previousDistance) <= 0.02f)
            {
                _atApproximatelySameDistanceCount++;
                Debug.Log($"hasn't moved {_atApproximatelySameDistanceCount}/50");
            }
            else
            {
                _atApproximatelySameDistanceCount = 0;
                _previousDistance = distance;
            }

            if(_atApproximatelySameDistanceCount > 50)
            {
                Debug.Log("stuck detected");
                _state.MovementTarget = null;
                _state.IsStuck = true;
                _atApproximatelySameDistanceCount = 0;
                _previousDistance = distance;
                RunState = eNodeState.FAILURE;
                return RunState;
            }

            Debug.Log("moving toward target");
            _state.MoveVal = (_state.MovementTarget.Value - _transform.position.ToVector2()).normalized;
            RunState = eNodeState.RUNNING;
            return RunState;
        }
    }
}
