using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameManager))]
public class SceneTransition : MonoBehaviour
{
    // public bool haveTransitionEffect;
    public float transistDuration = 1;
    ColorEffect colorEffect;
    TransformEffect transformEffect;
    GameManager gameManager;

    public enum TransitionEffect {Color, Transform};
    public enum TransformDirection {Up, Down, Left, Right};
    [SpaceAttribute]
    public TransitionEffect inEffect;
    public TransformDirection inDirection;
    [SpaceAttribute]
    public TransitionEffect outEffect;
    public TransformDirection outDirection;
    // [SerializeField] UnityEvent InSceneEffect;
    // [SerializeField] UnityEvent OutSceneEffect;
    [SpaceAttribute]
    public UnityEvent OnSceneChange;
    bool isLoading;
    
    // private static SceneTransition instance = null;
    void Awake()
    {
        // instance = this;
        colorEffect = GetComponentInChildren<ColorEffect>();
        transformEffect = GetComponentInChildren<TransformEffect>();
        gameManager = GetComponent<GameManager>();
        isLoading = false;
        // Debug.Log(instance);
        // Debug.Log(colorEffect);
    }
    void Start() {
        // gameManager.OnSceneLoaded += SceneLoadedEffect;
        SceneLoadedEffect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneLoadedEffect() {
        // if (colorEffect) {
        //     // white -> clear
        //     colorEffect.SetColor(Color.white);
        //     // colorEffect.SetFadeColor(Color.clear);
        //     // colorEffect.SetFadeDuration(transistDuration);
        //     colorEffect.StartFade(Color.clear, transistDuration);
        // }
        switch (inEffect) {
            case TransitionEffect.Color:
                // white -> clear
                colorEffect.SetColor(Color.white);
                colorEffect.StartFade(Color.clear, transistDuration);
                break;
            case TransitionEffect.Transform:
                switch (inDirection) {
                    case TransformDirection.Up:
                        transformEffect.UpOut();
                        break;
                    case TransformDirection.Down:
                        transformEffect.DownOut();
                        break;
                    case TransformDirection.Left:
                        transformEffect.LeftOut();
                        break;
                    case TransformDirection.Right:
                        transformEffect.RightOut();
                        break;
                }
                break;
        }
        // if (transformEffect) {
        //     transformEffect.DownIn();
        // }
    }

    public void ChangeScene(String sceneName) {
        if (isLoading) return;
        Debug.Log("Change to scene: " + sceneName);
        // todo: add callback (end effect)
        isLoading = true; 
        OnSceneChange?.Invoke();

        // colorEffect.StartFade(Color.white, transistDuration, ()=>{
        //     StartCoroutine(Loading(sceneName));
        // });

        switch (outEffect) {
            case TransitionEffect.Color:
                // white -> clear
                colorEffect.StartFade(Color.white, transistDuration, ()=>{
                    StartCoroutine(Loading(sceneName));
                });
                break;
            case TransitionEffect.Transform:
                switch (outDirection) {
                    case TransformDirection.Up:
                        transformEffect.UpIn(()=>{
                            StartCoroutine(Loading(sceneName));
                        });
                        break;
                    case TransformDirection.Down:
                        transformEffect.DownIn(()=>{
                            StartCoroutine(Loading(sceneName));
                        });
                        break;
                    case TransformDirection.Left:
                        transformEffect.LeftIn(()=>{
                            StartCoroutine(Loading(sceneName));
                        });
                        break;
                    case TransformDirection.Right:
                        transformEffect.RightIn(()=>{
                            StartCoroutine(Loading(sceneName));
                        });
                        break;
                }
                break;
        }
        // StartCoroutine(Loading(sceneName));
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
