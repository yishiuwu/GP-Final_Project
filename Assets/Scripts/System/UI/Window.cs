using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleWindow() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void CloseWindow() {
        gameObject.SetActive(false);
    }

    public void OpenWindow() {
        gameObject.SetActive(true);

    }
}
