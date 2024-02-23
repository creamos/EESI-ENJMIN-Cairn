using UnityEngine;
using UnityEngine.Audio;

public struct AudioRequestData
{
    public AudioClip clip;
    public float pitch;
    public AudioMixerGroup mixGroup;

    public AudioRequestData(AudioClip clip, float pitch = 1f, AudioMixerGroup mixGroup = null)
    {
        this.clip = clip;
        this.pitch = pitch;
        this.mixGroup = mixGroup;
    }
}