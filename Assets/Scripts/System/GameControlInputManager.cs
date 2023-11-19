using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GameManager))]
public class GameControlInputManager : MonoBehaviour
{
    // [SerializeField]
    // private InputActionAsset UIInput;
    [SerializeField]
    InputActionReference Pause;
    
    // Start is called before the first frame update
    void Start()
    {
        Pause.action.performed += (action)=>{
            if (GameManager.currentStage) GameManager.sceneTransition.ChangeScene("StageSelect");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
