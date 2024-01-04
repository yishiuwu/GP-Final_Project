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

    private string[] gotFeatureKeys = new string[]
    {
        "LabTrapGotten",
        "FanGotten",
        "ElectricityGotten",
        "ChemistryGotten"
    };

    public bool[] getFeatrue = {false, false, false, false};

    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < features.Length; i++){
            features[i].color = new Color(1, 1, 1, 0);
            getFeatrue[i] = false;
            // DataManager.Set(keys[i], 0);
            // DataManager.Set(keys[i] + "Gotten", 0);
        }
        Close();
        // CheckFeatures();
    }

    public void ClearRecord(){
        for(int i = 0; i < features.Length; i++){
            DataManager.Set(keys[i], 0);
            DataManager.Set(keys[i] + "Gotten", 0);
            features[i].color = new Color(1, 1, 1, 0);
            getFeatrue[i] = false;
        }
        CheckFeatures();
    }

    public void Open() {
        gameObject.SetActive(true);
        CheckFeatures();
    }
    public void Close() {
        gameObject.SetActive(false);
    }

    public void Toggle() {
        if (gameObject.activeSelf) Close();
        else Open();
    }
    

    public void CheckFeatures(){
        for(int i = 0; i < features.Length; i++){
            int got = 0;
            DataManager.Load(keys[i], 0, out got);
            Debug.Log(keys[i]);
            Debug.Log(got);

            if(/*DataManager.CheckValue(keys[i])*/ got == 1 && !getFeatrue[i]){
                Debug.Log("get stage cleared");
                Debug.Log(keys[i]);
                // have gotten feature before

                int gotten = 0;
                DataManager.Load(keys[i] + "Gotten", 0, out gotten);
                if(/*DataManager.CheckValue(keys[i] + "Gotten")*/ gotten == 1){
                    features[i].color = new Color(1, 1, 1, 1);
                }
                // get new feature
                else{
                    GetNewFeature(features[i]);
                    DataManager.Set(keys[i] + "Gotten", 1);
                }

                getFeatrue[i] = true;
            }
        }
    }

    public void GetNewFeature(Image feature){
        StartCoroutine(FadeInFeature(feature, 1.5f));
    }

    IEnumerator FadeInFeature(Image feature, float duration, Action callback = null){
        float startTime = Time.time;
        float t = (Time.time - startTime)/duration;
        
        // Debug.Log(t);
        while (t < 1) {
            feature.color = new Color(1, 1, 1, t);
            yield return null;
            t = (Time.time - startTime)/duration;
            // Debug.Log(t);
        }
        feature.color = new Color(1, 1, 1, 1);

        callback?.Invoke();
    }
}
