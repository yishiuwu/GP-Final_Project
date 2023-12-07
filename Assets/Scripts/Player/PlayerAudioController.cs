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
    
    public AudioClip walkSound;
    private bool walkSoundPlaying = false;
    
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
        
        if(playerstate.isWin && wincanplay) {
            myAudioSource.PlayOneShot(winSound);
            wincanplay = false;
            StartCoroutine(WaitForReplayWin());
        }
        if(playerstate.isRunning && !walkSoundPlaying){
            myAudioSource.loop = true;
            myAudioSource.clip = walkSound;
            myAudioSource.Play();
            walkSoundPlaying = true;
        }else{
            StartCoroutine(WaitForReplayWalk());
            
        }
    }
    
    IEnumerator WaitForReplayWin(){
        yield return new WaitForSeconds(2f);
        wincanplay = true;
        SceneManager.LoadScene("WinSceneTemp");
    }
    IEnumerator WaitForReplayWalk(){
        yield return new WaitForSeconds(1f);
        if(!playerstate.isRunning && walkSoundPlaying){
            walkSoundPlaying = false;
            myAudioSource.loop = false;
            myAudioSource.Stop();
        } 
    }
}
