using System;
using ScriptableEvents;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class CustomAudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips = {};
    public bool isPitchModulated = true;
    public AudioMixerGroup mixGroup;
    
    private int[] pentatonicSemitones = { 0, 2, 4, 7, 9 };

    [SerializeField] private AudioRequestEvent OnAudioRequested;

    public void Play()
    {
        if (audioClips.Length == 0) return;
        if (audioClips.Length == 1) Play(audioClips[0]);
        else Play(audioClips[Random.Range(0, audioClips.Length)]);
    }

    public void Play(AudioClip clip)
    {
        float pitch = 1f;
        if (isPitchModulated) pitch = ModulatePitch(pitch);
        
        OnAudioRequested?.Raise(new AudioRequestData(clip, pitch, mixGroup));
    }

    private float ModulatePitch(float pitch)
    {
        int semitoneOffset = pentatonicSemitones[Random.Range(0, pentatonicSemitones.Length)];
        for (int i = 0; i < semitoneOffset; i++)
        {
            pitch *= 1.059463f;
        }

        return pitch;
    }
}
