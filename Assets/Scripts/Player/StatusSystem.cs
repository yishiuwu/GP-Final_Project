using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusSystem : MonoBehaviour
{   
    [SerializeField] Player player;
    [SerializeField] private int HP;
    [SerializeField] private int maxHP;
    public bool isMelted = false;
    public bool isWin = false;
    public bool isLose = false;
    public bool isRunning = false;
    public Sprite meltImg;
    public Sprite solidImg;

    public string ph = "neutral";   // neutral/acid/alkali
    public event Action OnPh2N;
    public event Action OnPh2Ac;
    public event Action OnPh2Al;

    public static StatusSystem Instance;

    // Start is called before the first frame update
    void Start()
    {
        isWin = false;
        player = GetComponent<Player>();
        player.StatusChanging += OnStatusChanging;

        OnPh2N += ()=>{ph = "neutral";};
        OnPh2Ac += ()=>{ph = "acid";};
        OnPh2Al += ()=>{ph = "alkali";};

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMelted){
            GetComponent<SpriteRenderer>().sprite = meltImg;
        }else{
            GetComponent<SpriteRenderer>().sprite = solidImg;
        }
    }
    private void OnStatusChanging(object sender, StatusEventArgs e)
    
    {
        // print($"[(sender)]\t{e.actType} {e.target} {e.value}");
        if(e.target == "player"){
            if(e.actType==StatusEventArgs.ActType.Melt){
                isMelted = !isMelted;
            }else if(e.actType==StatusEventArgs.ActType.Win){
                isWin = true;
                isLose = false;
            }else if(e.actType==StatusEventArgs.ActType.Lose){
                isWin = false;
                isLose = true;
            }else if(e.actType==StatusEventArgs.ActType.Run){
                isRunning = true;
            }else if(e.actType==StatusEventArgs.ActType.Idle){
                isRunning = false;
            }
        }
        
    }

    public void Ph2N(){
        OnPh2N?.Invoke();
    }
    public void Ph2Ac(){
        OnPh2Ac?.Invoke();
    }
    public void Ph2Al(){
        OnPh2Al?.Invoke();
    }

}
public class StatusEventArgs : System.EventArgs{
    public enum ActType{
        Lose,
        Win,
        Run,
        Idle,
        Jump,
        Melt
    }
    public ActType actType;
    public string target;
    public int value;
    public StatusEventArgs(ActType _actTpe, string _target, int _value){
        actType = _actTpe;
        target = _target;
        value = _value;
    }
}