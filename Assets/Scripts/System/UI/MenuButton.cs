using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : Button
{
    public bool defaultSelected = false;

    // private Button btn;
    // Start is called before the first frame update
    // void Start()
    // {
    //     // btn = GetComponent<Button>();
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }
}
