using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusSystem : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject meltPlayer;
    [SerializeField] private GameObject playerforpos;
    // [SerializeField] private GameObject playerObject;
    // [SerializeField] private GameObject meltPlayerObject;
    [SerializeField] private int HP;
    [SerializeField] private int maxHP;
    public bool isMelted = false;
    public bool isWin = false;
    public bool isLose = false;
    public bool isRunning = false;
    public float new_x, new_y, new_z; //set for scale
    //public Sprite meltImg;
    //public Sprite solidImg;
    public GameObject meltCatPrefab;
    private GameObject meltCatInstance;
    private bool hasMelt = false;
    SpriteRenderer meltPlayerRenderer ;
    SpriteRenderer PlayerRenderer ;
    public string ph = "neutral";   // neutral/acid/alkali
    public event Action OnPh2N;
    public event Action OnPh2Ac;
    public event Action OnPh2Al;

    public static StatusSystem Instance;
    
    void Start()
    {
        isWin = false;
        player = GetComponentInChildren<Player>();
        player.StatusChanging += OnStatusChanging;
        meltPlayer.SetActive(false);
        PlayerRenderer = player.GetComponent<SpriteRenderer>();
        meltPlayerRenderer=meltPlayer.GetComponent<SpriteRenderer>();
        OnPh2N += ()=>{ph = "neutral";};
        OnPh2Ac += ()=>{ph = "acid";};
        OnPh2Al += ()=>{ph = "alkali";};

        Instance = this;
    }
    void Update()
    {

        if (isMelted)
        {
            //Vector3 meltPlayerPosition = meltPlayer.transform.position;
            GameObject mplayer = GameObject.FindGameObjectWithTag("bone");
            Vector3 meltPlayerPosition = mplayer.transform.position; // Get the meltPlayer's current position
            // Debug.Log($"update meltPlayerPosition: {meltPlayerPosition}");

            // Debug.Log($"meltPlayerPosition: {meltPlayerPosition}");
        }
        else
        {
            Vector3 playerPosition = playerforpos.transform.position;
            // Debug.Log($"playerPosition: {playerPosition}");
        }
    }
    private ParticleSystem liquidEffect;
    public ParticleSystem acidEffect;
    public ParticleSystem alkaliEffect;
    public GameObject ec;
    public GameObject ae;
    public GameObject ale;

    private void MeltPlayer()
    {
        GameObject splayer = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = splayer.transform.position; // Get the player's current position
        splayer.GetComponent<CapsuleCollider2D>().enabled = false;
        meltPlayer = Instantiate(meltCatPrefab,playerPosition, transform.rotation);
        Vector3 newScale = new Vector3(new_x, new_y, new_z);
        meltPlayer.transform.localScale = newScale;
        Debug.Log($"playerPosition: {playerPosition}");
        meltPlayer.transform.position = playerPosition; // Set meltPlayer's position to the player's position
        PlayerRenderer.sortingOrder = -1;

        if(ec != null){
            ae.transform.SetParent(meltPlayer.transform);
            ale.transform.SetParent(meltPlayer.transform);
            ae.transform.localPosition = Vector3.zero;
            ale.transform.localPosition = Vector3.zero;
            ae.transform.localScale = Vector3.one;
            ale.transform.localScale = Vector3.one;
        }

        // switch(ph){
        //     case "acid":
        //         liquidEffect = Instantiate(acidEffect, playerPosition, Quaternion.identity);
        //         liquidEffect.gameObject.SetActive(true);
        //         liquidEffect.Play();        // liquadAlkali.SetActive(false);
        //         Debug.Log("get liquid acid effect");
        //         break;
        //     case "alkali":
        //         liquidEffect = Instantiate(alkaliEffect, playerPosition, Quaternion.identity);
        //         liquidEffect.gameObject.SetActive(true);
        //         liquidEffect.Play();Debug.Log("get liquid alkali effect");
        //         break;
        // }
    }

    private void SolidifyPlayer()
    {
        GameObject mplayer = GameObject.FindGameObjectWithTag("bone");
        Vector3 meltPlayerPosition = mplayer.transform.position; // Get the meltPlayer's current position
        Debug.Log($"meltPlayerPosition: {meltPlayerPosition}");
        Destroy(meltPlayer);
        PlayerRenderer.sortingOrder = 7;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        player.transform.position = meltPlayerPosition; // Set player's position to the meltPlayer's position
        // Destroy(liquidEffect);
        if(ec != null){
            ae.transform.SetParent(ec.gameObject.transform);
            ale.transform.SetParent(ec.gameObject.transform);
        }
    }
    private void OnStatusChanging(object sender, StatusEventArgs e)
    
    {
        // print($"[(sender)]\t{e.actType} {e.target} {e.value}");
        if(e.target == "player"){
            if(e.actType==StatusEventArgs.ActType.Melt){
                isMelted = !isMelted;
                Debug.Log($"Player isMelted: {isMelted}");
                if (isMelted)
                {
                    MeltPlayer();
                }
                else
                {
                    SolidifyPlayer();
                }
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