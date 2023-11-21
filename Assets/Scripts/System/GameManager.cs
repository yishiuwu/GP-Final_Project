using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(SceneTransition))]
public class GameManager : MonoBehaviour
{
    public event Action OnSceneLoaded;
    public static Stage currentStage;
    public static SceneTransition sceneTransition;
    public string upperScene;
    // Start is called before the first frame update
    void Start()
    {
        sceneTransition = GetComponent<SceneTransition>();
    }

    public void LeaveGame() {
        Debug.Log("leaving game...");
        Application.Quit();
    }

    public void ToUpperScene() {
        sceneTransition.ChangeScene(upperScene);
    }
}
