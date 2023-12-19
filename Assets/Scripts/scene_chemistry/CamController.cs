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

    // Start is called before the first frame update
    void Start()
    {
        iniPos = new Vector3(3.540669f, 0.1898187f, -4.013027f);
        winPos = new Vector3(-8.6f, -19f, -4.013027f);

        transform.position = iniPos;
        win = false;
        gameWin = false;
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

    // void LateUpdate(){
    //     if(win){
    //         Vector3 pos = transform.position;
    //         transform.position = Vector3.Lerp(pos, winPos, Dump * Time.deltaTime);
    //     }
    // }

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

    IEnumerator CallGameWin(float waitTime){
        yield return new WaitForSeconds(waitTime);
        // Stage.OnWin?.Invoke();
    }
}
