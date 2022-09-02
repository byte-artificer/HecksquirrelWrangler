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
        public BoolValue PlayerWin;
        public AudioClip[] ScaredSounds;
        public AudioClip WarpOut;

        protected override void Start()
        {
            base.Start();
            State.Player = GameObject.FindGameObjectsWithTag("Player").First();
            State.DebugTarget = Instantiate(squirreltarget);
            State.DebugTarget.SetActive(false);
            State.Pen = GameObject.FindGameObjectsWithTag("Finish").First();
            State.PlayerWin = PlayerWin;
            State.ScaredSounds = ScaredSounds;

            heckSquirrelStates.Add(State);
        }

        protected override BehaviorTreeNode<HeckSquirrelState> SetupTreeCore()
        {
            var root =
                new SequenceNode<HeckSquirrelState>(State,
                    new SelectorNode<HeckSquirrelState>(State,
                        new SafeInPen(transform, State),
                        new SequenceNode<HeckSquirrelState>(State,
                            new FeelThreatened(transform, State, 1f, 1),
                            new FleeFromPlayer(transform, State, 1.75f)
                        ),
                        new SequenceNode<HeckSquirrelState>(State,
                            new FeelThreatened(transform, State, 2.5f, 0),
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

        bool _warping = false;
        protected override void Update()
        {
            base.Update();
            if(State.SafeInPen && !State.WarpedOut && !_warping)
            {
                _warping = true;
                AudioRequester.RequestedAudioClips.Enqueue(WarpOut);
                _animator.SetBool("warpOut", true);
            }
        }

        public void WarpOutDone()
        {
            State.WarpedOut = true;
            gameObject.SetActive(false);
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
