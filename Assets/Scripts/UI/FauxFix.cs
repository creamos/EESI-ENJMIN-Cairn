using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FauxFix : MonoBehaviour
{
    private Image image;
    
    public Sprite[] Frames;
    [SerializeField] private float fps = 6;

    private int frameID;
    private float lastChangeTime;
    
    [SerializeField]
    private bool isPlaying;
    public bool IsPlaying { 
        get => isPlaying;
        set
        {
            isPlaying = value;
            if (isPlaying)
            {
                lastChangeTime = Time.time;
                frameID = Math.Max(frameID, 0);
            }
        }
    }

    public void Initialize(Rock data)
    {
        Initialize(data.FrameList);
    }
    
    public void Initialize(Sprite[] frames)
    {
        Frames = frames;
        if (frames.Length > 0)
        {
            frameID = Math.Clamp(frameID, 0, frames.Length);
            image.sprite = frames[frameID];
        }
    }

    private void OnEnable()
    {
        image ??= GetComponent<Image>();
        if (isPlaying)
        {
            lastChangeTime = Time.time;
        }
        
        if (Frames.Length > 0) 
            image.sprite = Frames[0];
    }

    private void Update()
    {
        if (isPlaying && (Time.time - lastChangeTime) > 1f/fps)
        {
            if (Frames.Length == 0) return;
            frameID = (frameID+1)%Frames.Length;
            image.sprite = Frames[frameID];
            lastChangeTime = Time.time;
        }
    }
}
