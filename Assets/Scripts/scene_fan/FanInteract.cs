using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanInteract : InteractiveObj
{
    public AudioClip fanSound;
    public AudioClip BeepSound;
    // public AudioClip walkSound;
    public bool fanOn = false;
    public GameObject FanLeaf;
    private AudioSource myAudioSource;
    public GameObject player;
    private Rigidbody2D playerRb;
    // private StatusSystem playerstate;
    // private GameObject player;
    void Start(){
        myAudioSource = GetComponent<AudioSource>();
        playerRb = player.GetComponent<Rigidbody2D>();
        
    }
    void Update(){
        if(fanOn){
            FanLeaf.GetComponent<Animator>().SetBool("FanOn", true);
            if (playerRb.position.y <= -0.5f)
            {
                // 給予玩家施力（固定方向，大小為fanForce）
                Vector2 fanForceVector = new Vector2(-5f, 0f);
                playerRb.AddForce(fanForceVector*Time.deltaTime, ForceMode2D.Impulse);
                GameObject meltPlayer = GameObject.FindGameObjectWithTag("MeltPlayer");
                //mplayer.transform.position += new Vector3(-0.1f, 0f, 0f);
                if (meltPlayer != null)
                {
                    // Gradually move the "meltplayer" towards the negative x-axis
                    float meltPlayerSpeed = -2f; // Adjust the speed as needed
                    meltPlayer.transform.Translate(Vector3.right * meltPlayerSpeed * Time.deltaTime);
                }
                else
                {
                    Debug.LogWarning("MeltPlayer not found!");
                }
            }
        }else{
            FanLeaf.GetComponent<Animator>().SetBool("FanOn", false);
        }
    }
    public override void Interact()
    {
        // 在這裡實作該物體的具體互動行為
        if(fanOn){
            myAudioSource.Stop(); 
            myAudioSource.loop = false;
        }else{
            StartCoroutine(PlayFanSound());
        }
        fanOn = !fanOn;
    }
    IEnumerator PlayFanSound(){
        myAudioSource.PlayOneShot(BeepSound);
        yield return new WaitForSeconds(0.1f);
        myAudioSource.clip = fanSound;
        myAudioSource.loop = true;
        myAudioSource.Play();
    }
}
