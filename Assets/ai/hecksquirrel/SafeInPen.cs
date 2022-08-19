using Assets.extensions;
using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ai.hecksquirrel
{
    class SafeInPen : BehaviorTreeNode<HeckSquirrelState>
    {
        public SafeInPen(Transform transform, HeckSquirrelState state) : base(transform, state) { }

        public override eNodeState Evaluate()
        {
            var pos = _transform.position.ToVector2();
            if(!_state.SafeInPen)
                _state.SafeInPen = _state.Pen.GetComponent<Collider2D>().OverlapPoint(pos);

            if (_state.SafeInPen)
            {
                Debug.Log("Safely in pen");
                _state.CurrentSpeed = _state.BaseSpeed * 0.5f;
                _state.IsFleeing = false;

                RunState = eNodeState.SUCCESS;
                return RunState;
            }

            RunState = eNodeState.FAILURE;
            return RunState;
        }
    }
}
