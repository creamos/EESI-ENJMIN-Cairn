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
    public SelectPebbleButton currentStone;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;
    public GameObject textBox;
    public Button pickRockButton;

    public RockEvent OnPebbleAddedByPlayer;
    public GameEvent OnPanelOpened;
    
    private void OnEnable()
    {
        OnPanelOpened?.Raise();
    }

    // Start is called before the first frame update
    void Start()
    {
        //init listener stone to set text
        stonesUI = GameObject.FindGameObjectsWithTag("PickableStone");
        foreach (GameObject stone in stonesUI) 
        {
            Button stoneButton = stone.GetComponent<Button>();
            if (stoneButton)
            {
                stoneButton.onClick.AddListener(() => textBox.SetActive(true));
                SelectPebbleButton newSelectedStone = stone.GetComponent<SelectPebbleButton>();
                if (newSelectedStone) stoneButton.onClick.AddListener(() => UpdateText(newSelectedStone));
            }
        }
        
        // init when selected stone
        pickRockButton.onClick.RemoveAllListeners();
        pickRockButton.onClick.AddListener(() => SendRockData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(SelectPebbleButton newStone) 
    {
        //update old Select Stone button
        if (currentStone) currentStone._fauxFixController.IsPlaying = false;
        
        //update new Select Stone button
        currentStone = newStone;
        currentStone._fauxFixController.IsPlaying = true;
        
        // update text stone todo: uncomment when we have data text for Pebble
        // titleText.text = currentStone.Rock.prefabName;
        // bodyText.text = currentStone.Rock.rockDescription;
    }

    public void SendRockData () //datas to sendhere
    {
        // Reset rock selection panel
        if (currentStone) currentStone._fauxFixController.IsPlaying = false;
        gameObject.SetActive(false);
        textBox.SetActive(false);
        
        // Debug.Log("Datas sent: "+currentStone.Rock.prefabName);
        OnPebbleAddedByPlayer?.Raise(currentStone.Rock);
    }
}
