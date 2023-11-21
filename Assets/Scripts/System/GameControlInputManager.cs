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
    
    private void Awake() {
        // GameManager gameManager = FindObjectOfType<GameManager>();
        // Pause.action.performed += (action)=>{
        //     gameManager.ChangeScene(gameManager.upperScene);
        // };
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
