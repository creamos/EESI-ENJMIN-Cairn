using System;
using ScriptableEvents;
using UnityEngine;

public class CairnVisualizer : MonoBehaviour
{
    [SerializeField] private CairnData cairnData;
    [SerializeField] private Cairn cairnManager;
    [SerializeField] private PebbleLayer pebbleLayerPrefab;
    [SerializeField] private RectTransform layersContainer;

    [SerializeField] private GameEvent OnCairnModified;
    
    private void OnEnable()
    {
        if (OnCairnModified)
        {
            OnCairnModified.OnTriggered -= Rebuild;
            OnCairnModified.OnTriggered += Rebuild;
        }
    }

    private void OnDisable()
    {
        if (OnCairnModified)
        {
            OnCairnModified.OnTriggered -= Rebuild;
        }
    }


    public void AddPebble(Rock pebble)
    {
        Debug.Log(pebble.prefabName);
    }

    public void RemovePebble()
    {
        
    }

    public void Rebuild()
    {
        Canvas.ForceUpdateCanvases();
        float canvasWidth = layersContainer.rect.width*.9f;
        cairnData?.SetCanvasWidth(canvasWidth); 
        
        if (!cairnManager)
        {
            Debug.LogWarning($"{nameof(CairnVisualizer)}: No {nameof(Cairn)} referenced. " +
                             $"Cannot perform a rebuild.");
            return;
        }

        for(int i = layersContainer.childCount-1; i >= 0; --i)
        {
            Destroy(layersContainer.GetChild(i).gameObject);
        }

        int levelCount = cairnManager.GetFloorNumber();
        int visualLevelIndex = 0;
        int dataLevelIndex = 0;
        
        while (dataLevelIndex < levelCount)
        {
            Rock[] rocks = cairnManager.GetRocks(dataLevelIndex);
            if (rocks.Length == 0)
            {
                dataLevelIndex++;
                continue;
            }

            RenderLayer(visualLevelIndex, rocks);

            visualLevelIndex++;
            dataLevelIndex++;
        }
    }

    private void RenderLayer(int visualLevelIndex, Rock[] rocks)
    {
        var pebbleLayer = Instantiate(pebbleLayerPrefab, layersContainer);
        int index = 0;
        while (index < rocks.Length)
        {
            var rock = rocks[index];
            if (rock == null)
            {
                index++;
                continue;
            }
            
            pebbleLayer.AddRock(rock, false);
            index += rock.rockWidth;
        }
    }
}
