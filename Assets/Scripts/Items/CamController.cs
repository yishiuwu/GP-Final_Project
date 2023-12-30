using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CamController : MonoBehaviour
{
    Vector3 iniPos;
    Vector3 winPos;
    public float Dump = 0.6f;
    public bool win = false;
    public bool gameWin = false;

    public Vector3 meowPos;
    public GameObject player;

    public float iniCamSize = 5.0f;
    public float focusCamSize = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        iniPos = new Vector3(3.540669f, 0.1898187f, -4.013027f);
        winPos = new Vector3(-8.6f, -19f, -4.013027f);

        transform.position = iniPos;
        win = false;
        gameWin = false;

        Debug.Log(Camera.main.orthographicSize);
        Debug.Log(Camera.main.fieldOfView);
        Camera.main.orthographicSize = iniCamSize;
        
        StartCoroutine(WaitForTime(1.0f, ()=>{
            StartCam();
        }));
    }

    // Update is called once per frame
    void Update()
    {
        if(gameWin){
            gameWin = false;
            StartCoroutine(CallGameWin(1.0f));
        }
    }

    public void WinCam(){
        if(!win){
            StartCoroutine(WinMove(iniPos, winPos, 2, ()=>{gameWin = true;}));
            win = true;
        }
           
    }

    void StartCam(){
        meowPos = player.transform.position;
        Vector3 targetPos = new Vector3(meowPos.x, meowPos.y, iniPos.z);
        StartCoroutine(StartMove(iniPos, targetPos, iniCamSize, focusCamSize, 2, ()=>{
            Debug.Log("in");
            Debug.Log(Camera.main.orthographicSize);
            StartCoroutine(StartMove(targetPos, iniPos, focusCamSize, iniCamSize, 2, ()=>{ GameManager.currentStage.StartGame(); Debug.Log("out");}));
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

    IEnumerator CallGameWin(float waitTime){
        float startTime = Time.time;
        float t = (Time.time - startTime)/waitTime;
        
        while (t < 1) {
            yield return null;
            t = (Time.time - startTime)/waitTime;
        }
        
        DataManager.Set("LabTrap", 1);
        // int got = 0;
        // DataManager.Load("LabTrap", 0, out got);
        //     Debug.Log("LabTrap");
        //     Debug.Log(got);
        GameManager.currentStage.Win();
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
