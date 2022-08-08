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
    class GetUnstuck : BehaviorTreeNode<HeckSquirrelState>
    {

        public GetUnstuck(Transform transform, HeckSquirrelState state) : base(transform, state) 
        {
        }

        public override eNodeState Evaluate()
        {
            if(!_state.IsStuck)
            {
                RunState = eNodeState.FAILURE;
                return RunState;
            }

            Debug.Log("Trying to find unstuck target");

            _state.IsStuck = false;

            var direction = _state.MoveVal * -1;

            var res = new RaycastHit2D[0];

            var hits = _transform.gameObject.GetComponent<Collider2D>().Raycast(direction, res, 5);

            int maxTries = 24;
            int tries = 0;
            while(hits > 0 && tries < maxTries)
            {
                direction = Quaternion.Euler(0, 0, 360 / maxTries) * direction;
                hits = _transform.gameObject.GetComponent<Collider2D>().Raycast(direction, res, 5);
                tries++;
            }

            if(hits > 0)
            {
                Debug.Log("Failed to find appropriate rotation to get unstuck");
                RunState = eNodeState.FAILURE;
                return RunState;
            }

            var target = _transform.position + (direction.ToVector3() * 2);
            _state.MovementTarget = target;

            _state.DebugTarget.transform.position = target;
            _state.LastRetargetTime = Time.time;

            RunState = eNodeState.SUCCESS;
            return RunState;
        }
    }
}
