using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MeltPlayer : MonoBehaviour
{
    public StatusSystem status;
    // public PlayerAudioController audioCtr;
    [SerializeField] private float moveSpeed = 5f;
    public event System.EventHandler<StatusEventArgs> StatusChanging;
    private Rigidbody2D rb;
    private int moveDir = 0;
    void Start()
    {
        // Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<StatusSystem>();
        // audioCtr = GetComponent<PlayerAudioController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     PlayerMelt();
        // }

        if(status.isWin){
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 0;
            GetComponent<SpriteRenderer>().color = color;
            
        }
    }

    void FixedUpdate(){
        if(!status.isMelted){
            // if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            //     transform.Translate(moveSpeed*Time.deltaTime,0,0);
            //     GetComponent<SpriteRenderer>().flipX = true;
            //     SetRunning(true);
            // }else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            //     transform.Translate(-moveSpeed*Time.deltaTime,0,0);
            //     GetComponent<SpriteRenderer>().flipX = false;
            //     SetRunning(true);
            // }else{
            //     SetRunning(false);
            // }

            // if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
            //     Jump();
            // }
            if(moveDir != 0){
                transform.Translate(moveDir*moveSpeed*Time.deltaTime,0,0);
            }
        }else{
            // if(audioCtr.fanOn){
            //     BeMove(Vector2.left, 5f);
            // }
            SetRunning(false);
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="InteractiveObj" && !status.isMelted){
            
            InteractiveObj interactiveObj = other.gameObject.GetComponent<InteractiveObj>();
            if (interactiveObj != null)
            {
                interactiveObj.Interact();
            }
            
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="Exit" && status.isMelted){
            PlayerWin();
        }
    }
    public void RunLeft(InputAction.CallbackContext ctx){
        if(ctx.performed){
            if(status.isMelted) return;
            // transform.Translate(-moveSpeed*Time.deltaTime,0,0);
            moveDir = -1;
            GetComponent<SpriteRenderer>().flipX = false;
            SetRunning(true);
        }else if(ctx.canceled){
            moveDir = 0;
            SetRunning(false);
        }
    }
    public void RunRight(InputAction.CallbackContext ctx){
        if(ctx.performed){
            if(status.isMelted) return;
            // transform.Translate(moveSpeed*Time.deltaTime,0,0);
            moveDir = 1;
            GetComponent<SpriteRenderer>().flipX = true;
            SetRunning(true);
        }else if(ctx.canceled){
            moveDir = 0;
            SetRunning(false);
        }
    }
    
    public void PlayerMelt(InputAction.CallbackContext ctx){
        if(ctx.performed){
            if(this.StatusChanging != null){
                StatusChanging(this, new StatusEventArgs(StatusEventArgs.ActType.Melt, "player", 0));
            }
        }
        
    }
    public void PlayerWin(){
        if(this.StatusChanging != null){
            StatusChanging(this, new StatusEventArgs(StatusEventArgs.ActType.Win, "player", 0));
        }
    }
    void SetRunning(bool run){
        if(run){
            if(this.StatusChanging != null){
                StatusChanging(this, new StatusEventArgs(StatusEventArgs.ActType.Run, "player", 0));
            }
        }else{
            if(this.StatusChanging != null){
                StatusChanging(this, new StatusEventArgs(StatusEventArgs.ActType.Idle, "player", 0));
            }
        }
        
    }
    void Jump()
    {
        float jumpForce = 5f;
        rb.velocity = new Vector2(rb.velocity.x, 0f); // 將垂直速度歸零，防止累積
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    // void BeMove(Vector2 direction, float force){
    //     rb.AddForce(direction * force);
    // }
    

}
