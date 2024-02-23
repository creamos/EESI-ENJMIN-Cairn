using System.Collections;
using ScriptableEvents;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioRequestEvent OnAudioRequested;

    public void Play(AudioRequestData data) => Play(data.clip, data.pitch, data.mixGroup);
    public void Play(AudioClip clip, float pitch, AudioMixerGroup mixGroup)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = mixGroup;
        
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        audioSource.Play();
        StartCoroutine(RemoveAudioSourceOnComplete(audioSource));
    }

    private IEnumerator RemoveAudioSourceOnComplete(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(.1f);
        }
        
        Destroy(audioSource);
    }
}
