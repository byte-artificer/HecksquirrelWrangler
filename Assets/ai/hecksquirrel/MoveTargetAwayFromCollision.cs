using Assets.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai.hecksquirrel
{
    public class MoveTargetAwayFromCollision : BehaviorTreeNode
    {
        public static string moveAwayTarget = "moveAwayTarget";
        Transform _transform;
        float _targetDistance;
        public MoveTargetAwayFromCollision(Transform transform, float targetDistance)
        {
            _transform = transform;
            _targetDistance = targetDistance;
        }

        public override eNodeState Evaluate()
        {
            var collision = GetData("collision") as Collision2D;
            var moveVal = (Vector2)GetData("moveVal");

            if (collision != null)
            {
                var collisionNormal = collision.GetContact(0).normal;

                var movement = Vector2.Reflect(moveVal, collisionNormal); 

                //var movement = moveVal + collisionNormal;
                movement = movement.normalized;
                if (movement.Equals(Vector2.zero))
                    movement = Quaternion.Euler(0, 0, 45) * moveVal;

                var target = _transform.position + movement.ToVector3();

                Parent.SetData(moveAwayTarget, target);
                ClearData("collision");
            }

            return eNodeState.SUCCESS;
        }
    }
}
