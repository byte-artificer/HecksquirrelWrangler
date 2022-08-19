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
        [HideInInspector]
        public bool IsMoving;
        [HideInInspector]
        public bool IsStuck;
        [HideInInspector]
        public bool FacingLeft;
        public float CurrentSpeed;
        public float BaseSpeed;
        public Vector2 MoveVal;
        [HideInInspector]
        public Collision2D ActiveCollision;
        public Vector2? MovementTarget;

        public virtual void Reset()
        {
            IsMoving = false;
            IsStuck = false;
            FacingLeft = false;
            ActiveCollision = null;
            MovementTarget = null;
        }
    }
}
