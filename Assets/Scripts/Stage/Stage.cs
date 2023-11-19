using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public event Action OnLoaded;   // start effects
    public event Action OnStart;    // let player able to start playing (cant do any movement before calling this
    public event Action OnRestart;  // restart the stage (transition effect -> call onStart)
    public event Action OnWin;      // win effect -> call win menu
    public event Action OnTogglePause; // toggle pause
    // Start is called before the first frame update
    void Start()
    {
        GameManager.currentStage = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
