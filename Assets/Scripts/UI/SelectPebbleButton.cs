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

    private Image _image;
    private Button _button; 
    [FormerlySerializedAs("FauxFixController")] [HideInInspector]
    public FauxFix _fauxFixController;
    
    // Start is called before the first frame update
    void Start() 
    {
        _button = GetComponent<Button>();
        _fauxFixController = GetComponent<FauxFix>();
        if (rock) _fauxFixController.Initialize(rock);
        if (_button) _button.onClick.AddListener(OnActiveButton);
    }

    public void UpdateImage() {
        _image = GetComponent<Image>();
        _image.sprite = rock.FrameList[0];
    }
    
    private void OnValidate() {
        UpdateImage();
    }

    public void OnActiveButton() {
       
    }
}
