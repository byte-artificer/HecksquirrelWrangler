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
        public GameObject Player;
        public GameObject DebugTarget;
        public GameObject Pen;
        public bool IsFleeing;
        public bool IsPaused;
        public bool SafeInPen;
        public bool SquirrelsLose;
        public float LastRetargetTime = float.MaxValue;
    }
}
