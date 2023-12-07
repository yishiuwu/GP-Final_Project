using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : InteractiveObj
{
    public GameObject Light;
    public GameObject Player;
    public event System.EventHandler<StatusEventArgs> StatusChanging;
    public GameObject StageManager;
    private bool DoorOpen => (Light.GetComponent<LightsCtrl>().lightOn);
    private bool canOpen = true;
    public Sprite normalSprite; // 普通状态的图片
    public Sprite litSprite;    // 亮起状态的图片
    private SpriteRenderer spriteRenderer;
    public AudioClip OpenDoorSound;
    public AudioClip CloseDoorSound;
    private AudioSource myAudioSource;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DoorOpen && canOpen){
            canOpen = false;
            myAudioSource.PlayOneShot(OpenDoorSound);
            spriteRenderer.sprite = litSprite;
            spriteRenderer.sortingOrder = 3;
        }
        if(!DoorOpen && !canOpen){
            canOpen = true;
            myAudioSource.PlayOneShot(CloseDoorSound);
            spriteRenderer.sprite = normalSprite;
            spriteRenderer.sortingOrder = 3;
        }
        
    }
    public override void Interact()
    {
        // 在這裡實作該物體的具體互動行為
        if(DoorOpen){
            
            Player.GetComponent<Player>().PlayerWin();
            
            
        }else{
            return;
        }
    }
    
    
}
