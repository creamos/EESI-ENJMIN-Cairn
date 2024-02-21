using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Rock")]
public class Rock : ScriptableObject
{
    public string prefabName;
    public int rockWidth; // de 1 à 4
    
    // Ajouter l'image, et ce dont a besoin viktor pour les anim
    public Animator animationControl;
}
