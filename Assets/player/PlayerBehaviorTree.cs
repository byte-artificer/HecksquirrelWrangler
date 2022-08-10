using Assets.ai;
using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.player
{
    public class PlayerBehaviorTree : BaseEntity<PlayerState>
    {
        public EnemyStateCollection HeckSquirrelStates;
        public FloatValue PlayerStamina;
        bool win;

        protected override void Start()
        {
            base.Start();
            State.HeckSquirrelStates = HeckSquirrelStates;
            State.PlayerStamina = PlayerStamina;
        }

        protected override void Update()
        {
            base.Update();

            if (State.WinLevel)
            {
                _animator.SetBool("win", true);
                win = true;

                PlayerStamina.Value = 1;

                base.HandleMovement(Vector2.zero);
            }
        }

        protected override BehaviorTreeNode<PlayerState> SetupTreeCore()
        {
            return new SelectorNode<PlayerState>(State,
                    new WinLevel(transform, State),
                    new SequenceNode<PlayerState>(State,
                        new SelectorNode<PlayerState>(State,
                            new SequenceNode<PlayerState>(State,
                                new ShouldSprint(transform, State),
                                new SprintAndExpendStamina(transform, State)
                            ),
                            new SequenceNode<PlayerState>(State,
                                new NormalMovement(transform, State),
                                new ShouldRecoverStamina(transform, State),
                                new RecoverStamina(transform, State)
                            ),
                            new NormalMovement(transform, State)
                        ),
                        new ClampStaminaAndUpdateDisplayValue(transform, State),
                        new DoMovement(transform, State)
                    )
                );
        }

        void OnMove(InputValue value)
        {
            if (win)
                return;

            State.MovementInput = value?.Get<Vector2>();
        }

        void OnSprint(InputValue value)
        {
            if (win)
                return;

            State.SprintHeld = value.isPressed;
        }
    }
}
