using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Clip Requester")]
public class AudioRequester : ScriptableObject
{

    public static string MusicVolumeKey = "musicVolume";
    public static string SFXVolumeKey = "sfxVolume";

    public ConcurrentQueue<AudioClip> RequestedAudioClips = new ConcurrentQueue<AudioClip>();

    private void OnEnable()
    {
        RequestedAudioClips.Clear();
    }
}
