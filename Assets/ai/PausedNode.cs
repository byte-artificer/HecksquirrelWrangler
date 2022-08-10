using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai
{
    public class PausedNode<T> : BehaviorTreeNode<T> where T : BaseEntityState
    {
        BoolValue _pauseTracking;

        public PausedNode(Transform transform, T state, BoolValue pauseTracking) : base(transform, state) { _pauseTracking = pauseTracking; }

        public override eNodeState Evaluate()
        {
            RunState = _pauseTracking.Value ? eNodeState.RUNNING : eNodeState.FAILURE;
            return RunState;
        }

    }
}
