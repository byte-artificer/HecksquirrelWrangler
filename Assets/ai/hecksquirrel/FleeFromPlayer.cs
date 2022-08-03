using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai.hecksquirrel
{
    public class FleeFromPlayer : BehaviorTreeNode
    {
        GameObject _player;
        public FleeFromPlayer(GameObject player)
        {
            _player = player;
        }

        public override eNodeState Evaluate()
        {
            return eNodeState.RUNNING;
        }
    }
}
