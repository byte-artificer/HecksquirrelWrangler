using Assets.extensions;
using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai.hecksquirrel
{
    public class MoveTargetAwayFromCollision : BehaviorTreeNode<HeckSquirrelState>
    {
        public MoveTargetAwayFromCollision(Transform transform, HeckSquirrelState state) : base(transform, state) { }
        public override eNodeState Evaluate()
        {
            var collision = _state.ActiveCollision;
            var moveVal = _state.MoveVal;

            if (collision != null)
            {
                Debug.Log("moving away from collision");

                var collisionNormal = collision.GetContact(0).normal;

                var movement = Vector2.Reflect(moveVal, collisionNormal); 

                movement = movement.normalized;
                if (movement.Equals(Vector2.zero))
                    movement = Quaternion.Euler(0, 0, 45) * moveVal;

                var target = _transform.position + (movement.ToVector3() * 2);

                _state.DebugTarget.transform.position = target;
                _state.LastRetargetTime = Time.time;

                _state.MovementTarget = target.ToVector2();
                _state.ActiveCollision = null; //Collision handled
            }

            RunState = eNodeState.SUCCESS;
            return RunState;
        }
    }
}
