using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private FauxFix _fauxFixController;
    
    // Start is called before the first frame update
    void Start() 
    {
        _button = GetComponent<Button>();
        _fauxFixController = GetComponent<FauxFix>();
        if (rock) _fauxFixController.Initialize(rock);
        if (_button) _button.onClick.AddListener(OnActiveButton);
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    void OnActiveButton() {
        if (_fauxFixController) _fauxFixController.IsPlaying = !_fauxFixController.IsPlaying;
    }
}
