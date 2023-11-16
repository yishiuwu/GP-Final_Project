using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAudioController : MonoBehaviour
{
    // Sounds
    // [SerializeField] public AudioClip jumpSound;
    public AudioClip winSound;
    // public AudioClip loseSound;
    public AudioClip fanSound;
    public bool fanOn = false;
    public GameObject FanLeaf;
    private AudioSource myAudioSource;
    private StatusSystem playerstate;
    bool wincanplay = true;
    // TODO: Get Jumper component and register onJump event to play SFX.
    // Hint: You can use onJump Action from Jumper to call the callback function.
    void Start(){
        myAudioSource = GetComponent<AudioSource>();
        playerstate = GetComponent<StatusSystem>();
    }
    void Update(){
        if(fanOn){
            FanLeaf.GetComponent<Animator>().SetBool("FanOn", true);
        }else{
            FanLeaf.GetComponent<Animator>().SetBool("FanOn", false);
        }
        if(playerstate.isWin && wincanplay) {
            myAudioSource.PlayOneShot(winSound);
            wincanplay = false;
            StartCoroutine(WaitForReplayWin());
        }
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="Fan" && !playerstate.isMelted){
            
            if(fanOn){
               myAudioSource.Stop(); 
               myAudioSource.loop = false;
            }else{
                myAudioSource.loop = true;
                myAudioSource.clip = fanSound;
                myAudioSource.Play();
                
            }
            fanOn = !fanOn;
            
        }

    }
    IEnumerator WaitForReplayWin(){
        yield return new WaitForSeconds(3f);
        wincanplay = true;
        SceneManager.LoadScene("WinSceneTemp");
    }
}
