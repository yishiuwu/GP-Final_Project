using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Book : MonoBehaviour
{
    public Image[] features;

    public string[] keys = new string[]
    {
        "LabTrap",
        "Fan",
        "Electricity",
        "Chemistry"
    };

    public bool[] getFeatrue = {false, false, false, false, false};

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < features.Length; i++){
            features[i].canvasRenderer.SetAlpha(0);
            getFeatrue[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckFeatures(){
        for(int i = 0; i < features.Length; i++){
            if(DataManager.CheckValue(keys[i]) && !getFeatrue[i]){
                features[i].canvasRenderer.SetAlpha(1);
            }
        }
    }

    public void GetNewFeature(string key){
        for(int i = 0; i < features.Length; i++){
            if(keys[i] == key){
                if(DataManager.CheckValue(keys[i]) && !getFeatrue[i]){
                    StartCoroutine(FadeInFeature(features[i], 1.5f));
                }
            }
        }
    }

    IEnumerator FadeInFeature(Image feature, float duration, Action callback = null){

        feature.CrossFadeAlpha(1.0f, duration, false);

        yield return new WaitForSeconds(duration);

        callback?.Invoke();
    }
}
