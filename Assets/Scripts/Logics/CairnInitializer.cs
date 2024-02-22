using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Cairn))]
public class CairnInitializer : MonoBehaviour
{
    [SerializeField] private CairnData cairnData;
    [SerializeField] private PebbleRegistry registry;

    private Cairn cairn;

    private void Start()
    {
        cairn = GetComponent<Cairn>();
        if (!cairn) return;

        if (cairnData)
        {
            cairnData.Load();
            
            bool shouldReset = cairnData.randomCairnOnStart;
            if (cairnData.loadedPebbleIDs.Count == 0 || shouldReset)
            {
                RandomFill(5);
                cairn.Save();
            }

            else
            {
                FillFromList(cairnData.loadedPebbleIDs);
            }
        }
    }

    private void RandomFill(int count)
    {
        int i = 0;
        List<Rock> spawnedRocks = new List<Rock>(count);
        while (i < count)
        {
            Rock rock;
            do {
                rock = registry.Pebbles[Random.Range(0, registry.Pebbles.Count)];
            } while (spawnedRocks.Contains(rock));
            
            spawnedRocks.Add(rock);
            cairn.AddRock(rock, false);
            
            i++;
        }
    }

    private void FillFromList(List<int> pebbleIDs)
    {
        if (!registry) return;

        int registrySize = registry.Pebbles.Count;
        foreach (var pebbleID in pebbleIDs)
        {
            if (pebbleID >= registrySize || pebbleID < 0) continue;
            var pebble = registry.Pebbles[pebbleID];
            cairn.AddRock(pebble, false);
        }
    }
}
