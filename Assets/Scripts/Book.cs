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

    public bool[] getFeatrue = {false, false, false, false, false};

    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < features.Length; i++){
            features[i].canvasRenderer.SetAlpha(0);
            getFeatrue[i] = false;
        }
        Close();
        // CheckFeatures();
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
            if(DataManager.CheckValue(keys[i]) && !getFeatrue[i]){
                // have gotten feature before
                if(DataManager.CheckValue(keys[i] + "Gotten")){
                    features[i].canvasRenderer.SetAlpha(1);
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

        feature.CrossFadeAlpha(1.0f, duration, false);

        yield return new WaitForSeconds(duration);

        callback?.Invoke();
    }
}
