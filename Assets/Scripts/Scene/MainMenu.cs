using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public GameObject defaultSelectedButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelectButton(defaultSelectedButton));
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string sceneName) {
        GameManager.sceneTransition.ChangeScene(sceneName);
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
