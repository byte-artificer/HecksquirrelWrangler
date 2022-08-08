using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.state
{
    [System.Serializable]
    public class BaseEntityState
    {
        public bool IsMoving;
        public bool IsStuck;
        public bool FacingLeft;
        public float CurrentSpeed;
        public float BaseSpeed;
        public Vector2 MoveVal;
        public Collision2D ActiveCollision;
        public Vector2? MovementTarget;
    }
}
