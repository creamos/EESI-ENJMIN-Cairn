using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public void Toggle (GameObject menu) 
    { 
        menu.SetActive (!menu.activeInHierarchy);
    }
}
