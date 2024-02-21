using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectPebbleButton : MonoBehaviour
{
    [SerializeField]
    private Rock rock;
    public Rock Rock 
    { 
        get => rock;
        set
        {
            rock = value;
        }
    }

    private Button _button;
    [HideInInspector]
    public FauxFix FauxFixController;
    
    // Start is called before the first frame update
    void Start() 
    {
        _button = GetComponent<Button>();
        FauxFixController = GetComponent<FauxFix>();
        if (rock) FauxFixController.Initialize(rock);
        if (_button) _button.onClick.AddListener(OnActiveButton);
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void OnActiveButton() {
       
    }
}
