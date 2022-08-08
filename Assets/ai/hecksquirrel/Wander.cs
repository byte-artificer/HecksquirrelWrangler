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
    public class Wander : BehaviorTreeNode<HeckSquirrelState>
    {

        public Wander(Transform transform, HeckSquirrelState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            var target = _transform.position.ToVector2();

            var dieRoll = (int)(Random.value * 100);
            if (!_state.MoveVal.Equals(Vector2.zero) && dieRoll <= 75)
            {
                Debug.Log("finding rotated wander target");
                var rotated = Quaternion.Euler(0, 0, Random.Range(-30, 30)) * (_state.MoveVal * 2);
                target = (_state.MovementTarget ?? target) + rotated.ToVector2();
            }
            else
            {
                Debug.Log("finding random wander target");
                target = target + (Random.onUnitSphere.ToVector2() * 2); ;
            }

            dieRoll = (int)(Random.value * 100);
            if(_state.SafeInPen && dieRoll < 85)
            {
                if(!_state.Pen.GetComponent<Collider2D>().OverlapPoint(target))
                    target = _transform.position.ToVector2();
            }


            _state.DebugTarget.transform.position = target;
            _state.LastRetargetTime = Time.time;

            _state.MovementTarget = target;
            RunState = eNodeState.SUCCESS;
            return RunState;
        }
    }
}
