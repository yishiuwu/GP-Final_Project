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
        Debug.Log($"CheckPos: {CheckPos()}, IsMelt: {playerstate.isMelted}");
        if(CheckPos() && playerstate.isMelted==true){
            Linked = true;
            spriteRenderer.sprite = litSprite;
            spriteRenderer.sortingOrder = 2;
        }else{
            Linked = false;
            spriteRenderer.sprite = normalSprite;
            spriteRenderer.sortingOrder = 2;
        }
        
    }
    bool CheckPos(){
        Debug.Log($"Pos: {mplayer.transform.position}");
        if(mplayer.transform.position.x<0f && mplayer.transform.position.x>-0.6f){
            if(mplayer.transform.position.y < 0f && mplayer.transform.position.y > -3.2f){
                return true;
            }
        }
        return false;
    }
}
