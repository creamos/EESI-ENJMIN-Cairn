using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pebbles/Pebble Data")]
public class Rock : ScriptableObject
{
    public string prefabName;
    [TextArea]
    public string rockDescription;
    public int rockWidth; // de 1 à 4
    public AudioClip RockAudioCLip; //Son de rock unique
    
    // Ajouter l'image, et ce dont a besoin viktor pour les anim
    public AnimationClip RockAnimClip;
    public Sprite[] FrameList;
}
