using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.state
{
    [System.Serializable]
    public class HeckSquirrelState : BaseEntityState
    {
        [HideInInspector]
        public GameObject Player;
        [HideInInspector]
        public GameObject DebugTarget;
        [HideInInspector]
        public GameObject Pen;
        [HideInInspector]
        public bool IsFleeing;
        [HideInInspector]
        public bool IsPaused;
        [HideInInspector]
        public bool SafeInPen;
        [HideInInspector]
        public bool WarpedOut;
        [HideInInspector]
        public BoolValue PlayerWin;
        [HideInInspector]
        public float LastRetargetTime = float.MaxValue;

        public override void Reset()
        {
            base.Reset();
            IsFleeing = false;
            IsPaused = false;
            SafeInPen = false;
            WarpedOut = false;
            LastRetargetTime = float.MaxValue;
        }
    }
}
