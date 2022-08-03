using Assets.ai.hecksquirrel;
using Assets.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.ai
{
    public class MovementTypeTreeNodeBase : BehaviorTreeNode
    {
        public Vector2? MovementTarget { get; private set; }
        protected Transform _transform;
        protected BaseEntity _owner;

        public MovementTypeTreeNodeBase(Transform transform, BaseEntity owner)
        {
            _transform = transform;
            _owner = owner;
        }

        public override eNodeState Evaluate()
        {
            var currentPosition2D = _transform.position.ToVector2();
            var moveAwayTarget = GetData(MoveTargetAwayFromCollision.moveAwayTarget) as Vector3?;

            if (moveAwayTarget.HasValue)
            {
                MovementTarget = moveAwayTarget.Value.ToVector2();
                ClearData(MoveTargetAwayFromCollision.moveAwayTarget);
            }

            if (MovementTarget == null)
            {
                MovementTarget = GetNextMovementTarget();
            }

            if (Vector2.Distance(currentPosition2D, MovementTarget.Value) <= 0.01f)
            {
                MovementTarget = GetNextMovementTarget();
            }

            _owner.moveVal =(MovementTarget.Value - _transform.position.ToVector2()).normalized;
            State = eNodeState.RUNNING;
            return State;
        }

        protected virtual Vector2 GetNextMovementTarget() { return _transform.position; }
    }
}
