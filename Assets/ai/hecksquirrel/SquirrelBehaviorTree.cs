using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai.hecksquirrel
{
    public class SquirrelBehaviorTree : BaseEntity
    {
        GameObject _player;

        protected override void Start()
        {
            base.Start();
            _player = GameObject.FindGameObjectsWithTag("Player").First();
        }

        protected override BehaviorTreeNode SetupTree()
        {
            var root = new SequenceNode(new List<BehaviorTreeNode> 
            { 
                new MoveTargetAwayFromCollision(transform, 1.3f), 
                new Wander(transform, 1f, this) 
            });
            return root;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Root.SetData("collision", col);
        }
    }
}
