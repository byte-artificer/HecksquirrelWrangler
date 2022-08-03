using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.extensions
{
    public static class VectorExtensions
    {
        public static Vector2 ToVector2(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Vector3 ToVector3(this Vector2 vec)
        {
            return new Vector3(vec.x, vec.y, 0);
        }
    }
}
