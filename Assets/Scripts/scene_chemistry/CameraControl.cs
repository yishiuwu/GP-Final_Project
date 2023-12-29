using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour
{
    public Vector3 iniPos;
    public float Dump = 0.6f;

    public float iniCamSize = 5.0f;

    public Vector3 paperPos;
    public Vector3 powderPos;
    public float papreFocusSize = 3.3f;
    public float powderFocusSize = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(iniPos == null) iniPos = new Vector3(-0.4f, 0.3f, -10.0f);
        if(paperPos == null) paperPos = new Vector3(-4.5f, -1.0f, -10.0f);
        if(powderPos == null) powderPos = new Vector3(5.0f, -1.0f, -10.0f);

        transform.position = iniPos;
        Camera.main.orthographicSize = iniCamSize;

        StartCoroutine(WaitForTime(1.0f, ()=>{
            StartCam();
        }));
    }

    // Update is called once per frame
    void Update()
    {

    }


    void StartCam(){
        // Focus on paper
        StartCoroutine(StartMove(iniPos, paperPos, iniCamSize, papreFocusSize, 2, ()=>{
            StartCoroutine(StartMove(paperPos, iniPos, papreFocusSize, iniCamSize, 2, ()=>{ 
                //Focus on powder
                StartCoroutine(StartMove(iniPos, powderPos, iniCamSize, powderFocusSize, 2, ()=>{
                    StartCoroutine(StartMove(powderPos, iniPos, powderFocusSize, iniCamSize, 2, ()=>{GameManager.currentStage.StartGame();}));
                }));
            }));
        }));
    }

    static float EaseOutQuint(float t) => 1 - Mathf.Pow(1 - t, 5);
    IEnumerator WinMove(Vector3 from, Vector3 to, float duration, Action callback = null) {
        float startTime = Time.time;
        float t = (Time.time - startTime)/duration;
        
        // Debug.Log(t);
        while (t < 1) {
            transform.position = Vector3.Lerp(from, to, EaseOutQuint(t));
            yield return null;
            t = (Time.time - startTime)/duration;
            // Debug.Log(t);
        }
        transform.position = to;
        callback?.Invoke();
    }

    IEnumerator StartMove(Vector3 from, Vector3 to, float fromSize, float toSize, float duration, Action callback = null) {
        float startTime = Time.time;
        float t = (Time.time - startTime)/duration;
        
        // Debug.Log(t);
        while (t < 1) {
            transform.position = Vector3.Lerp(from, to, EaseOutQuint(t));
            Camera.main.orthographicSize = Mathf.Lerp(fromSize, toSize, EaseOutQuint(t));
            yield return null;
            t = (Time.time - startTime)/duration;
            // Debug.Log(t);
        }
        transform.position = to;
        callback?.Invoke();
    }


    IEnumerator WaitForTime(float waitTime, Action callback = null){
        float startTime = Time.time;
        float t = (Time.time - startTime)/waitTime;
        
        while (t < 1) {
            yield return null;
            t = (Time.time - startTime)/waitTime;
        }

        callback?.Invoke();
    }
}
