using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FingerScrollManager : MonoBehaviour, IPointerDownHandler
{
    public Scrollbar scrollbar;
    Vector2 origin;
    bool scrolling = false;
    float scrollOrigine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scrolling) 
        { 
            
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        origin = eventData.pressPosition;
        scrollOrigine = scrollbar.value;
    }



}
