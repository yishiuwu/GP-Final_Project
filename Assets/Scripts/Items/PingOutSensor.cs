using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingOutSensor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WinAfterSeconds(float duration, System.Action callback = null){
        Debug.Log("leave ping");

        float t = 0;
        float startTime = Time.time;

        while(t < 1){
            yield return null;
            t = (Time.time - startTime) / duration;
        }
        callback?.Invoke();
    }


    void OnTriggerEnter2D(Collider2D other){
        Debug.Log(other);
        if(/*other.gameObject.tag == "Player"*/ other.gameObject.tag == "bone"){
            Debug.Log("get melt meow");
            StartCoroutine(CallGameWin(2.0f));
        }
    }

    IEnumerator CallGameWin(float waitTime){
        float startTime = Time.time;
        float t = (Time.time - startTime)/waitTime;
        
        while (t < 1) {
            yield return null;
            t = (Time.time - startTime)/waitTime;
        }
        
        DataManager.Set("LabTrap", 1);
        GameManager.currentStage.Win();
    }
}
