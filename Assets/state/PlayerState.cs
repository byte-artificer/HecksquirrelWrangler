using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.state
{
    [System.Serializable]
    public class PlayerState : BaseEntityState
    {
        public float MaxStamina = 5f;
        public float Stamina = 5f;
        [HideInInspector]
        public bool  Sprinting = false;
        [HideInInspector]
        public bool  SprintHeld = false;
        [HideInInspector]
        public FloatValue PlayerStamina;
        [HideInInspector]
        public EnemyStateCollection HeckSquirrelStates;
        public Vector2? MovementInput;
        [HideInInspector]
        public bool WinLevel = false;

        public override void Reset()
        {
            base.Reset();

            Stamina = MaxStamina;
            Sprinting = false;
            SprintHeld = false;
            HeckSquirrelStates = new EnemyStateCollection();
            MovementInput = null;
            WinLevel = false;
        }
    }
}
