using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameManager))]
public class SceneTransition : MonoBehaviour
{
    // public bool haveTransitionEffect;
    public float transistDuration = 1;
    ColorEffect colorEffect;
    GameManager gameManager;

    bool isLoading;
    
    // private static SceneTransition instance = null;
    void Awake()
    {
        // instance = this;
        colorEffect = GetComponentInChildren<ColorEffect>();
        gameManager = GetComponent<GameManager>();
        isLoading = false;
        // Debug.Log(instance);
        // Debug.Log(colorEffect);
    }
    void Start() {
        gameManager.OnSceneLoaded += SceneLoadedEffect;
        SceneLoadedEffect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneLoadedEffect() {
        if (colorEffect) {
            // white -> clear
            colorEffect.SetColor(Color.white);
            colorEffect.SetFadeColor(Color.clear);
            colorEffect.SetFadeDuration(transistDuration);
        }
    }

    public void ChangeScene(String sceneName) {
        if (isLoading) return;
        Debug.Log("Change to scene: " + sceneName);
        // todo: add callback (end effect)
        isLoading = true; 
        StartCoroutine(Loading(sceneName));
    }


    ///// DEBUG /////
    // public string nextScene;
    // [ContextMenu("test change scene")]
    // public void TestChangeScene() {
    //     Debug.Log("Change to scene: " + nextScene);
    //     // callback: end effect 
    //     StartCoroutine(Loading(nextScene));
    // }
    IEnumerator Loading(string sceneName, Action callback = null) {
        yield return null;
        AsyncOperation task = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        task.allowSceneActivation = false;
        while (!task.isDone)
        {
            // [0, 0.9] > [0, 1]
            // float progress = Mathf.Clamp01(ao.progress / 0.9f);
            // Debug.log("Loading progress: " + (progress * 100) + "%");
            // do something for effect

            // Loading completed
            if (task.progress == 0.9f)
            {
                // Debug.Log("Press a key to start");
                // if (Input.AnyKey())
                task.allowSceneActivation = true;
            }
            yield return null;
        }
        callback?.Invoke();
        isLoading = false;
    }
    ///// DEBUG /////
}
