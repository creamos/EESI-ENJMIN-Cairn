using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoneTextBox : MonoBehaviour
{
    public static UnityEvent stoneInfos;
    public GameObject[] stonesUI;
    public GameObject stonePannel;
    // Start is called before the first frame update
    void Start()
    {
        stonesUI = GameObject.FindGameObjectsWithTag("PickableStone");
    }

    // Update is called once per frame
    void Update()
    {
        if (stonePannel!= null && stonePannel.activeInHierarchy && stonesUI.Length==0) 
        {
            
        }
    }
}
