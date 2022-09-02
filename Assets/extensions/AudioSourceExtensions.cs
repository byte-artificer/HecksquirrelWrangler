using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.extensions
{
    public static class AudioSourceExtensions
    {
        public static IEnumerator WaitForSound(this AudioSource source, Action callback)
        {
            while (source.isPlaying)
                yield return null;

            callback();
        }
    }
}
