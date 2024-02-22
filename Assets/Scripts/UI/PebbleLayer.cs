using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleLayer : MonoBehaviour
{
    [SerializeField] private CairnPebbleButton pebblePrefab;
    [SerializeField] private Transform container;
    
    public void AddRock(Rock data, bool activeState)
    {
        var pebble = Instantiate(pebblePrefab, container);
        pebble.Initialize(data, activeState);
    }
}
