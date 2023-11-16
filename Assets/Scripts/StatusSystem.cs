using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        isWin = false;
        player = GetComponent<Player>();
        player.StatusChanging += OnStatusChanging;
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