using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(SceneTransition))]
public class GameManager : MonoBehaviour
{
    public event Action OnSceneLoaded;
    public static Stage currentStage;
    public static SceneTransition sceneTransition;
    public string upperScene;
    public AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        sceneTransition = GetComponent<SceneTransition>();
        Menu menu = GetComponentInChildren<Menu>();
        if (menu) {
            menu.OnOpen += MouseOn;
            menu.OnClose += MouseOff;
        }
        MouseOff();
    }

    public void LeaveGame() {
        Debug.Log("leaving game...");
        Application.Quit();
    }

    public void ToUpperScene() {
        sceneTransition.ChangeScene(upperScene);
    }

    public void MouseOff() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MouseOn() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
