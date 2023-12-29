using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLineCtrl : MonoBehaviour
{
    public bool Linked = false;
    public GameObject player;
    public GameObject mplayer;
    private StatusSystem playerstate;
    public Sprite normalSprite; // 普通状态的图片
    public Sprite litSprite;    // 亮起状态的图片
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        playerstate = player.GetComponent<StatusSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerstate.isMelted==true){
            mplayer = GameObject.FindGameObjectWithTag("MeltPlayer");
            if(CheckPos(mplayer)){
                GameObject objectToDeactivate = GameObject.Find("tips");
                if(objectToDeactivate != null) objectToDeactivate.SetActive(false);
                Debug.Log($"CheckPos: {CheckPos(mplayer)}, IsMelt: {playerstate.isMelted}");
                Linked = true;
                spriteRenderer.sprite = litSprite;
                spriteRenderer.sortingOrder = 3;
            }
            
        }else{
            Linked = false;
            spriteRenderer.sprite = normalSprite;
            spriteRenderer.sortingOrder = 3;
        }
        
    }
    bool CheckPos(GameObject tar){
        Debug.Log($"mPos: {tar.transform.position}");
        if(tar.transform.position.x<0f && tar.transform.position.x>-0.6f){
            if(tar.transform.position.y < 0f && tar.transform.position.y > -3.2f){
                return true;
            }
        }
        return false;
    }
}
