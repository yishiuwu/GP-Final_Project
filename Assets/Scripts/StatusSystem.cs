using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSystem : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject meltPlayer;
    // [SerializeField] private GameObject playerObject;
    // [SerializeField] private GameObject meltPlayerObject;
    [SerializeField] private int HP;
    [SerializeField] private int maxHP;
    public bool isMelted = false;
    public bool isWin = false;
    public bool isLose = false;
    public bool isRunning = false;
    //public Sprite meltImg;
    //public Sprite solidImg;
    public GameObject meltCatPrefab;
    private GameObject meltCatInstance;
    private bool hasMelt = false;
    SpriteRenderer meltPlayerRenderer ;
    SpriteRenderer PlayerRenderer ;

    void Start()
    {
        isWin = false;
        player = GetComponentInChildren<Player>();
        player.StatusChanging += OnStatusChanging;
        meltPlayer.SetActive(false);
        PlayerRenderer = player.GetComponent<SpriteRenderer>();
    }
    // Vector3 mplayerPosition;
    // Vector3 playerPosition;
    void Update()
    {
        // playerPosition = player.transform.position;
        // mplayerPosition = meltPlayer.transform.position;
        if (isMelted)
        {
            // meltPlayer.SetActive(true);
            // PlayerRenderer.sortingOrder = -1;
            Vector3 meltPlayerPosition = meltPlayer.transform.position;
            Debug.Log($"meltPlayerPosition: {meltPlayerPosition}");
        }
        else
        {
            // meltPlayer.SetActive(false);
            //PlayerRenderer.sortingOrder = 3;
            Vector3 playerPosition = player.transform.position;
            Debug.Log($"playerPosition: {playerPosition}");
        }
    }

    private void MeltPlayer()
    {
        Vector3 playerPosition = player.transform.position; // Get the player's current position
        Vector3 playerCenter = GetObjectCenter(player.gameObject); // Get the center of the player
        Debug.Log($"playerPosition: {playerPosition}");
        meltPlayer.transform.position = playerPosition; // Set meltPlayer's position to the player's position
        SetObjectCenter(meltPlayer.gameObject, playerCenter); // Set the center of meltPlayer to player's center
        meltPlayer.SetActive(true);
        PlayerRenderer.sortingOrder = -1;
    }

    private void SolidifyPlayer()
    {
        Vector3 meltPlayerPosition = meltPlayer.transform.position; // Get the meltPlayer's current position
        Vector3 meltPlayerCenter = GetObjectCenter(meltPlayer.gameObject); // Get the center of meltPlayer
        Debug.Log($"meltPlayerPosition: {meltPlayerPosition}");
        meltPlayer.SetActive(false);
        PlayerRenderer.sortingOrder = 3;

        player.transform.position = meltPlayerPosition; // Set player's position to the meltPlayer's position
        SetObjectCenter(player.gameObject, meltPlayerCenter); // Set the center of player to meltPlayer's center
    }
    private Vector3 GetObjectCenter(GameObject obj)
    {
        Bounds bounds = obj.GetComponent<Renderer>().bounds;
        return bounds.center;
    }

    // Helper method to set the center of a GameObject
    private void SetObjectCenter(GameObject obj, Vector3 center)
    {
        Bounds bounds = obj.GetComponent<Renderer>().bounds;
        Vector3 offset = center - bounds.center;
        obj.transform.position += offset;
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
                    // playerPosition = player.transform.position;
                    // meltPlayer.SetActive(true);
                    // PlayerRenderer.sortingOrder = -1;
                    // meltPlayer.transform.position = playerPosition;
                    MeltPlayer();
                }
                else
                {
                    // mplayerPosition = meltPlayer.transform.position;
                    // meltPlayer.SetActive(false);
                    // PlayerRenderer.sortingOrder = 3;
                    // player.transform.position = mplayerPosition;
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