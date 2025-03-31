using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject defaultSelectedButton;
    public Button[] btns;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelectButton(defaultSelectedButton));
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null) {
            gameManager.menu.OnOpen += OnMenuOpen;
            gameManager.menu.OnClose += OnMenuClose;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string sceneName) {
        GameManager.sceneTransition.ChangeScene(sceneName);
    }

    void OnMenuOpen() {
        foreach (Button btn in btns) {
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.None;
            btn.navigation = nav;
        }
    }
    void OnMenuClose() {
        foreach (Button btn in btns) {
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Automatic;
            btn.navigation = nav;
        }
    }

    IEnumerator SelectButton(GameObject dftSelectBtn) {
        EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
        GameObject lastSelectedGameObject = dftSelectBtn;
        while (true) {
            yield return new WaitUntil(()=>EventSystem.current.currentSelectedGameObject == null);
            if (EventSystem.current.currentSelectedGameObject == null) {
                EventSystem.current.SetSelectedGameObject(lastSelectedGameObject);
            Debug.Log("click");
            }
        }
    }
}
