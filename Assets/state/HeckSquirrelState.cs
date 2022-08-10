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
        public bool IsFleeing;
        public bool IsPaused;
        public bool SafeInPen;
        [HideInInspector]
        public BoolValue PlayerWin;
        public float LastRetargetTime = float.MaxValue;
    }
}
