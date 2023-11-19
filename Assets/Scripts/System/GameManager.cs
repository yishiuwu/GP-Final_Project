using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

[RequireComponent(typeof(DontBreak))]
[RequireComponent(typeof(SceneTransition))]
public class GameManager : MonoBehaviour
{
    public event Action OnSceneLoaded;
    public static Stage currentStage;
    public static SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        sceneTransition = GetComponent<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
