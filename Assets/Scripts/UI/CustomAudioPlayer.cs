using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class CustomAudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips = {};
    public bool isPitchModulated = true;
    
    private AudioSource source;
    private int[] pentatonicSemitones = { 0, 2, 4, 7, 9 };
    
    private void OnEnable()
    {
        source ??= GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (audioClips.Length == 0) return;
        if (audioClips.Length == 1) Play(audioClips[0]);
        else Play(audioClips[Random.Range(0, audioClips.Length)]);
    }

    public void Play(AudioClip clip)
    {
        if (!source) return;
        
        source.Stop();
        if (isPitchModulated) ModulatePitch();
        source.clip = clip;
        source.Play();
    }

    private void ModulatePitch()
    {
        source.pitch = 1;
        int semitoneOffset = pentatonicSemitones[Random.Range(0, pentatonicSemitones.Length)];
        for (int i = 0; i < semitoneOffset; i++)
        {
            source.pitch *= 1.059463f;
        }
    }
}
