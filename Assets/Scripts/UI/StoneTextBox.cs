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
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;
    public GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (stonesUI.Length == 0)
        {
            stonesUI = GameObject.FindGameObjectsWithTag("PickableStone");
            if (stonesUI.Length > 0)
            {
                foreach (GameObject stone in stonesUI)
                {
                    if (stone.GetComponent<Button>())
                    {
                        stone.GetComponent<Button>().onClick.AddListener(() => textBox.SetActive(true));
                        stone.GetComponent<Button>().onClick.AddListener(() => UpdateText(stone.name, "test"));
                    }
                }
            }
        }
        
    }

    public void UpdateText(string title, string body) 
    {
        titleText.text = title;
        bodyText.text = body;
    }
}
