using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Cairn))]
public class CairnInitializer : MonoBehaviour
{
    [SerializeField] private PebbleRegistry registry;

    private void Start()
    {
        var cairn = GetComponent<Cairn>();
        if (!cairn) return;

        int i = 0;
        List<Rock> spawnedRocks = new List<Rock>(5);
        while (i < 5)
        {
            Rock rock;
            do {
                rock = registry.Pebbles[Random.Range(0, registry.Pebbles.Count)];
            } while (spawnedRocks.Contains(rock));
            
            cairn.AddRock(rock);
            
            i++;
        }
    }
}
