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
        public bool  Sprinting = false;
        public bool  SprintHeld = false;
        [HideInInspector]
        public FloatValue PlayerStamina;
        [HideInInspector]
        public EnemyStateCollection HeckSquirrelStates;
        public Vector2? MovementInput;
        public bool WinLevel = false;
    }
}
