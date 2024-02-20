using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

public class StoneTextBox : MonoBehaviour
{
    public GameObject[] stonesUI;
    public GameObject stonePannel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;
    // Start is called before the first frame update
    void Start()
    {
        stonesUI = GameObject.FindGameObjectsWithTag("PickableStone");
        stonePannel = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (stonePannel!= null && stonePannel.activeInHierarchy && stonesUI.Length==0) 
        {
            stonesUI = GameObject.FindGameObjectsWithTag("PickableStone");
        }
        else if (stonePannel == null)
        {
            stonePannel = transform.parent.gameObject;
        }
    }

    public void UpdateText(string title, string body) 
    {
        titleText.text = title;
        bodyText.text = body;
    }
}
