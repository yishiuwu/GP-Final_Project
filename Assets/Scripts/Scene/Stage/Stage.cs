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

    public string nextStage;

    [Space]
    [SerializeField]
    private Menu pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private WordMovement startMove;
    [SerializeField] private WordMovement winMove;
    [SerializeField] private Book book;


    void Start() {
        GameManager.currentStage = this;
        FindObjectOfType<GameManager>().upperScene = "StageSelect";
        winMenu.SetActive(false);

        OnRestart += ()=>{OnStart?.Invoke();}; // need change scene effect
        OnTogglePause += ()=>{pauseMenu.Toggle();};
        // OnWin += ()=>{winMenu.SetActive(true);}
        OnStart += ()=>{startMove.StartMove();};
        OnWin += ()=>{winMove.StartMove(()=>{StartCoroutine(WinRoutine());});};
    }

    void OnDestroy() {
        GameManager.currentStage = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePause() {
        OnTogglePause?.Invoke();
    }

    public void StartGame() {
        Debug.Log("start game");
        OnStart?.Invoke();
    }

    public void Win() {
        OnWin?.Invoke();
        // StartCoroutine(WinRoutine());
    }
    IEnumerator WinRoutine() {
        book.Open();
        yield return new WaitForSeconds(3.0f);
        LeaveStage();
        yield break;
    }

    public void Restart() {
        OnRestart?.Invoke();
    }

    public void NextStage() {
        GameManager.sceneTransition.ChangeScene(nextStage);
    }
    public void LeaveStage() {
        GameManager.sceneTransition.ChangeScene("StageSelect");
    }
    
}
