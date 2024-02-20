using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarManager : MonoBehaviour
{
    private Scrollbar currentScrollbar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScrollbar (Scrollbar _scrollbar) 
    {
        currentScrollbar = _scrollbar;
    }

    public void ScrollObj (GameObject _scrollingObj)
    {
        float _x = _scrollingObj.transform.localPosition.x;
        float _z = _scrollingObj.transform.localPosition.z;
        float _objPosY = currentScrollbar.value * 1500;
        _scrollingObj.transform.localPosition = new Vector3(_x,_objPosY,_z);
    }

}
