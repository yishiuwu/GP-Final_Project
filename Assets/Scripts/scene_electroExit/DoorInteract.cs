using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorInteract : InteractiveObj
{
    public GameObject Light;
    public GameObject Player;
    public GameObject DoorSkin;
    public GameObject StageManager;
    private bool DoorOpen => (Light.GetComponent<LightsCtrl>().lightOn);
    private bool canOpen = true;
    // public Sprite normalSprite; // 普通状态的图片
    // public Sprite litSprite;    // 亮起状态的图片
    // private SpriteRenderer spriteRenderer;
    public AudioClip OpenDoorSound;
    public AudioClip CloseDoorSound;
    private AudioSource myAudioSource;
    void Start()
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DoorOpen && canOpen){
            canOpen = false;
            myAudioSource.PlayOneShot(OpenDoorSound);
            GetComponent<Animator>().SetBool("DoorOpen", true);
            StartCoroutine(WaitForEmptyit());
            // spriteRenderer.sprite = litSprite;
            // spriteRenderer.sortingOrder = 3;
        }
        if(!DoorOpen && !canOpen){
            canOpen = true;
            myAudioSource.PlayOneShot(CloseDoorSound);
            // spriteRenderer.sprite = normalSprite;
            // spriteRenderer.sortingOrder = 3;
            GetComponent<Animator>().SetBool("DoorClose", true);
            StartCoroutine(WaitForShowUp());
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
    IEnumerator WaitForEmptyit(){
        yield return new WaitForSeconds(4f);
        GetComponent<Animator>().SetBool("DoorOpen", false);
        // DoorSkin.SetActive(false);
    }
    IEnumerator WaitForShowUp(){
        yield return new WaitForSeconds(2f);
        GetComponent<Animator>().SetBool("DoorClose", false);
        // DoorSkin.SetActive(true);
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "bone"){
            if(DoorOpen){
                StartCoroutine(WaitForTime(1.0f, ()=>{
                    DataManager.Set("Electricity", 1);
                    GameManager.currentStage.Win();
                }));
            }
        }
    }

    IEnumerator WaitForTime(float waitTime, Action callback = null){
        float startTime = Time.time;
        float t = (Time.time - startTime)/waitTime;
        
        while (t < 1) {
            yield return null;
            t = (Time.time - startTime)/waitTime;
        }

        callback?.Invoke();
    }
}
