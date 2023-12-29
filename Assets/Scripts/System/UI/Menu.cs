using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public bool isOpen = false;
    public event Action OnOpen, OnClose;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        OnOpen += ()=>{gameObject.SetActive(true);};
        OnOpen += gameManager.MouseOn;
        OnClose += ()=>{gameObject.SetActive(false);};
        OnClose += gameManager.MouseOff;
        Close();
    }

    void Awake() {
        // OnOpen += ()=>{gameObject.SetActive(true);};
        // OnClose += ()=>{gameObject.SetActive(false);};
    }


    public void Toggle() {
        if (isOpen) Close();
        else Open();
    }

    public void Open() {
        isOpen = true;
        OnOpen?.Invoke();
    }

    public void Close() {
        isOpen = false;
        OnClose?.Invoke();
    }
}
