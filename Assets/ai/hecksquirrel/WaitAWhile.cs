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
    public class WaitAWhile : BehaviorTreeNode<HeckSquirrelState>
    {
        bool _paused;
        float _pausedFor;
        int _consecutivePauses = 1;
        float _pauseTime;

        public WaitAWhile(Transform transform, HeckSquirrelState state, float pauseTime) : base(transform, state) { _pauseTime = pauseTime; }

        public override eNodeState Evaluate()
        {
            if(_state.PlayerWin.Value)
            {
                _state.MoveVal = Vector2.zero;
                _state.MovementTarget = _transform.position;
                RunState = eNodeState.RUNNING;
                return RunState;
            }

            if(_state.MovementTarget.HasValue)
            {
                RunState = eNodeState.FAILURE;
                return RunState;
            }

            if (_paused)
            {
                _pausedFor += Time.deltaTime;
                Debug.Log($"resting {_pausedFor} / {_pauseTime}");
            }

            if (!_paused || _pausedFor >= _pauseTime)
            {
                _paused = false;
                _state.IsPaused = false;
                _pausedFor = 0;

                var dieRoll = (int)(Random.value * 100);
                if (dieRoll <= (45/_consecutivePauses))
                {
                    Debug.Log($"start resting {dieRoll} <= {(45 / _consecutivePauses)}");
                    _consecutivePauses++;
                    _paused = true;
                    _state.IsPaused = true;
                }
                else
                {
                    Debug.Log("not resting");
                    _consecutivePauses = 1;
                    RunState = eNodeState.FAILURE;
                    return RunState;
                }
            }

            _state.MoveVal = Vector2.zero;
            RunState = eNodeState.RUNNING;
            return RunState;
        }
    }
}
