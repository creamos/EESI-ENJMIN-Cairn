using System;
using System.Collections.Generic;
using ScriptableEvents;
using UnityEngine;

[CreateAssetMenu(menuName = "Pebbles/Cairn Data")]
public class CairnData : ScriptableObject
{
    public const string PEBBLE_COUNT_SAVE = "SAVE_PEBBLE_COUNT";
    public const string PEBBLE_DATA_SAVE_PREFIX = "SAVE_PEBBLE_";

    public PebbleRegistry registry;
    public int cairnWidth = 9;
    [NonSerialized] public float canvasWidth = 1024;
    public Action OnCanvasWidthUpdated;
    public FloatEvent OnCanvasWidthUpdatedEvent;

    [NonSerialized] public List<int> loadedPebbleIDs;
    [NonSerialized] private bool isMuted;
    [SerializeField] public BoolEvent OnMutedStateChanged; 

    public bool IsMuted
    {
        get => isMuted;
        set
        {
            if (value != isMuted)
            {
                isMuted = value;
                OnMutedStateChanged?.Raise(isMuted);
            }
        }
    }

    public bool randomCairnOnStart = true;
    
    public void Load()
    {
        loadedPebbleIDs = new List<int>();
#if !UNITY_WEBGL
        if (!PlayerPrefs.HasKey(PEBBLE_COUNT_SAVE)) return;
        int pebbleCount = PlayerPrefs.GetInt(PEBBLE_COUNT_SAVE);
        for (int i = 0; i < pebbleCount; i++)
        {
            if (PlayerPrefs.HasKey(PEBBLE_DATA_SAVE_PREFIX + i))
                loadedPebbleIDs.Add(PlayerPrefs.GetInt(PEBBLE_DATA_SAVE_PREFIX + i));
        }
#endif
    }

    public void Save(Rock[] pebbles)
    {
        if (!registry)
        {
            Debug.LogWarning($"{nameof(CairnData)}: No {nameof(PebbleRegistry)} referenced, can't perform a save !");
            return;
        }

        int validPebbles = 0;
        for (int i=0; i < pebbles.Length; ++i)
        {
            Rock pebble = pebbles[i];
            int index = registry.Pebbles.FindIndex((Rock pebbleInRegistry) => pebble == pebbleInRegistry);
            if (index == -1)
                continue;

            validPebbles++;
            PlayerPrefs.SetInt(PEBBLE_DATA_SAVE_PREFIX + i, index);
        }
        
        PlayerPrefs.SetInt(PEBBLE_COUNT_SAVE, validPebbles);
    }

    public void SetCanvasWidth(float width)
    {
        if (Math.Abs(canvasWidth - width) > 0.001f)
        {
            canvasWidth = width;
            OnCanvasWidthUpdated?.Invoke();
            OnCanvasWidthUpdatedEvent?.Raise(canvasWidth);
        }
        
    }
}
