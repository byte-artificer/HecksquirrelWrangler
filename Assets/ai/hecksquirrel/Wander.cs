using Assets.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.ai.hecksquirrel
{
    public class Wander : MovementTypeTreeNodeBase
    {
        bool _paused;
        float _pausedFor;
        float _pauseTime;

        public Wander(Transform transform, float pauseTime, BaseEntity owner) : base(transform, owner)
        {
            _pauseTime = pauseTime;
        }

        public override eNodeState Evaluate()
        {
            if(_paused)
            {
                _pausedFor += Time.deltaTime;

                if(_pausedFor >= _pauseTime)
                {
                    _paused = false;
                }

                _owner.moveVal = Vector2.zero;
                State = eNodeState.RUNNING;
            }
            else
            {
                State = base.Evaluate();
            }
            
            return State;
        }

        protected override Vector2 GetNextMovementTarget()
        {
            var target = _transform.position.ToVector2();

            var dieRoll = (int)(Random.value * 100);
            if (dieRoll <= 45)
            {
                _paused = true;
                _pausedFor = 0;
            }
            else if (dieRoll <= 95)
            {
                var rotated = Quaternion.Euler(0, 0, Random.Range(-30, 30)) * (_owner.moveVal * 2);
                target = (MovementTarget ?? target) + rotated.ToVector2();
            }
            else
            {
                target = target + (Random.onUnitSphere.ToVector2() * 2); ;
            }

            return target;
        }
    }
}
