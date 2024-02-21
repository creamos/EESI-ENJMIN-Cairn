using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using ScriptableEvents;
using TMPro;
using Random = UnityEngine.Random;

public class StoneTextBox : MonoBehaviour
{
    public GameObject[] stonesUI;
    public GameObject currentStone;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;
    public GameObject textBox;
    public Button pickRockButton;

    public RockEvent OnPebbleAddedByPlayer;

    [Header("[TODO] Remove when valid pebbles can be selected from UI")] [SerializeField]
    private PebbleRegistry registry;
   
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
                        stone.GetComponent<Button>().onClick.AddListener(() => UpdateText(stone.name, "test",stone));
                    }
                }
            }
        }
        
    }

    public void UpdateText(string title, string body, GameObject stone) 
    {
        SelectPebbleButton buttonManager;
        //update old Select Stone button
        if (currentStone) {
            buttonManager = currentStone.GetComponent<SelectPebbleButton>();
            if (buttonManager) buttonManager.FauxFixController.IsPlaying = false;
        }
        currentStone = stone;
        
        //update new Select Stone button
        if (currentStone) {
            buttonManager = currentStone.GetComponent<SelectPebbleButton>();
            if (buttonManager) buttonManager.FauxFixController.IsPlaying = true;
        }
        
        //enable currentStone animation
        pickRockButton.onClick.RemoveAllListeners();
        titleText.text = title;
        bodyText.text = body;
        pickRockButton.onClick.AddListener(() => SendRockData(stone));
        pickRockButton.onClick.AddListener(() => gameObject.SetActive(false));
        pickRockButton.onClick.AddListener(() => textBox.SetActive(false));
    }

    public void SendRockData (GameObject stone) //datas to sendhere
    {
        Rock RandomPebble() => registry.Pebbles[Random.Range(0, registry.Pebbles.Count)];
        
        Debug.Log("Datas sent: "+stone.name);
        OnPebbleAddedByPlayer?.Raise(RandomPebble());
    }
}
