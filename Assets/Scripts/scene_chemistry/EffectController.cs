using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectController : MonoBehaviour
{
    public GameObject solidAcid;
    public GameObject solidAlkali;
    public GameObject liquadAcid;
    public GameObject liquadAlkali;

    public event Action DisableParticalEffect;
    public event Action EnableParticalEffect;

    public static EffectController Instance;

    // Start is called before the first frame update
    void Start()
    {
        solidAcid.SetActive(false);
        solidAlkali.SetActive(false);
        liquadAcid.SetActive(false);
        liquadAlkali.SetActive(false);

        DisableParticalEffect += ()=>{
            solidAcid.SetActive(false);
            solidAlkali.SetActive(false);
            liquadAcid.SetActive(false);
            liquadAlkali.SetActive(false);
            Debug.Log("remove effect");
        };

        EnableParticalEffect += ()=>{
            if(!StatusSystem.Instance.isMelted){
                switch(StatusSystem.Instance.ph){
                    case "acid":
                        solidAcid.SetActive(true);
                        solidAlkali.SetActive(false);
                        liquadAcid.SetActive(false);
                        liquadAlkali.SetActive(false);
                        Debug.Log("get solid acid effect");
                        break;
                    case "alkali":
                        solidAcid.SetActive(false);
                        solidAlkali.SetActive(true);
                        liquadAcid.SetActive(false);
                        liquadAlkali.SetActive(false);
                        Debug.Log("get solid alkali effect");
                        break;
                }
            }
            else if(StatusSystem.Instance.isMelted){
                switch(StatusSystem.Instance.ph){
                    case "acid":
                        solidAcid.SetActive(false);
                        solidAlkali.SetActive(false);
                        liquadAcid.SetActive(true);
                        liquadAlkali.SetActive(false);
                        Debug.Log("get melt acid effect");
                        break;
                    case "alkali":
                        solidAcid.SetActive(false);
                        solidAlkali.SetActive(false);
                        liquadAcid.SetActive(false);
                        liquadAlkali.SetActive(true);
                        Debug.Log("get melt alkali effect");
                        break;
                }
            }
        };

        Instance = this;
    }

    public void getPartical(){
        EnableParticalEffect?.Invoke();
    }

    public void removePartical(){
        DisableParticalEffect?.Invoke();
    }

    // [TODO] before switch status
    // EffectController.Instance.removePartical();
    // [TODO] after switch status
    // EffectController.Instance.getPartical(); 

    // Update is called once per frame
    void Update()
    {
        if(StatusSystem.Instance.ph == "neutral"){
            solidAcid.SetActive(false);
            solidAlkali.SetActive(false);
            liquadAcid.SetActive(false);
            liquadAlkali.SetActive(false);
        }
        else if(StatusSystem.Instance.ph == "acid"){
            if(!StatusSystem.Instance.isMelted){
                solidAcid.SetActive(true);
                solidAlkali.SetActive(false);
                liquadAcid.SetActive(false);
                liquadAlkali.SetActive(false);  
            }
            else{
                solidAcid.SetActive(false);
                solidAlkali.SetActive(false);
                liquadAcid.SetActive(true);
                liquadAlkali.SetActive(false);  
            }
        }
        else if(StatusSystem.Instance.ph == "alkali"){
            if(!StatusSystem.Instance.isMelted){
                solidAcid.SetActive(false);
                solidAlkali.SetActive(true);
                liquadAcid.SetActive(false);
                liquadAlkali.SetActive(false);  
            }
            else{
                solidAcid.SetActive(false);
                solidAlkali.SetActive(false);
                liquadAcid.SetActive(false);
                liquadAlkali.SetActive(true);  
            }
        }
    }
}
