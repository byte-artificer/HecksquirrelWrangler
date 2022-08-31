using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Clip Requester")]
public class AudioRequester : ScriptableObject
{
    public ConcurrentQueue<AudioClip> RequestedAudioClips = new ConcurrentQueue<AudioClip>();

    private void OnEnable()
    {
        RequestedAudioClips.Clear();
    }
}
