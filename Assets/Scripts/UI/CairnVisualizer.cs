using UnityEngine;

public class CairnVisualizer : MonoBehaviour
{
    [SerializeField] private Cairn cairnManager;
    
    
    public void AddPebble(Rock pebble)
    {
        Debug.Log(pebble.prefabName);
    }

    public void RemovePebble()
    {
        
    }

    public void Rebuild()
    {
        if (!cairnManager)
        {
            Debug.LogWarning($"{nameof(CairnVisualizer)}: No {nameof(Cairn)} referenced. " +
                             $"Cannot perform a rebuild.");
            return;
        }

        const int levelCount = 5 /*cairnManagement.GetFloorNumber()*/;
        int visualLevelIndex = 0;
        int dataLevelIndex = 0;
        
        while (dataLevelIndex < levelCount)
        {
            Rock[] rocks = { }; // cairnManagement.GetRocksAtLocation(dataLevelIndex);
            if (rocks.Length == 0)
            {
                dataLevelIndex++;
                continue;
            }

            RenderLevel(visualLevelIndex, rocks);

            visualLevelIndex++;
            dataLevelIndex++;
        }
    }

    private void RenderLevel(int visualLevelIndex, Rock[] rocks)
    {
        
    }
}
