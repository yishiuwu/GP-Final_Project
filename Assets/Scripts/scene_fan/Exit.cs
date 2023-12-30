using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        // Debug.Log(other.gameObject.tag);
        if(/*other.gameObject.tag == "Player" || */other.gameObject.tag == "bone" && StatusSystem.Instance.isMelted){
            StartCoroutine(WaitForTime(1.0f, ()=>{
                DataManager.Set("Fan", 1);
                GameManager.currentStage.Win();
            }));
        }
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
