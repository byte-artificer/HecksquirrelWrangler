using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai.hecksquirrel
{
    public class SquirrelBehaviorTree : BaseEntity<HeckSquirrelState>
    {
        public GameObject squirreltarget;
        public EnemyStateCollection heckSquirrelStates;

        protected override void Start()
        {
            base.Start();
            State.Player = GameObject.FindGameObjectsWithTag("Player").First();
            State.DebugTarget = Instantiate(squirreltarget);
            State.Pen = GameObject.FindGameObjectsWithTag("Finish").First();
            heckSquirrelStates.Add(State);
        }

        protected override BehaviorTreeNode<HeckSquirrelState> SetupTree()
        {
            var root =
                new SequenceNode<HeckSquirrelState>(State,
                    new SelectorNode<HeckSquirrelState>(State,
                        new SafeInPen(transform, State),
                        new SequenceNode<HeckSquirrelState>(State,
                            new FeelThreatened(transform, State, 1f),
                            new FleeFromPlayer(transform, State, 1.75f)
                        ),
                        new SequenceNode<HeckSquirrelState>(State,
                            new FeelThreatened(transform, State, 2.5f),
                            new FleeFromPlayer(transform, State, 1.25f)
                        ),
                        new FeelSafe(transform, State)
                    ),
                    new MoveTargetAwayFromCollision(transform, State),
                    new SelectorNode<HeckSquirrelState>(State,
                        new DoMovementBehavior<HeckSquirrelState>(transform, State),
                        new GetUnstuck(transform, State),
                        new WaitAWhile(transform, State, 0.5f),
                        new Wander(transform, State)
                    )
                );

            return root;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Finish")
                State.SafeInPen = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Finish")
                State.SafeInPen = false;
        }
    }
}
