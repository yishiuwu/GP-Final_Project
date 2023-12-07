using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumper : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected CollisionState collisionState;
    [SerializeField]
    private AnimationCurve jumpCurve;
    [SerializeField][Min(0)]
    private float jumpDuration = 0.75f;
    private float jumpStartTime = float.NegativeInfinity;
    public bool isJumping => (Time.fixedTime < jumpStartTime + jumpDuration) && !collisionState.grounded;
    public bool isRising => !isFalling && !collisionState.grounded;
    public bool isFalling => rb.velocity.y <= 0 && !collisionState.grounded;
    private int JumpTime = 1;
    public virtual bool canJump => collisionState.grounded;
    private StatusSystem playerState ;
    // public event System.Action onJump;
    void Start(){}
    void Update(){
        // if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
            
        //     if(JumpTime > 0 && !playerState.isMelted){
        //         Jump();
        //     }   
    
        // }
    }
    protected virtual void DoJump(){
        if(!(canJump&& !playerState.isMelted)){//&& JumpTime > 0 
                return;
        } 
        JumpTime -= 1;
        jumpStartTime = Time.fixedTime;
        // Initial impulse
        rb.velocity = new Vector2(rb.velocity.x, jumpCurve.Evaluate(0)*20f);
        // onJump?.Invoke();
       
    }
    public void Jump(InputAction.CallbackContext ctx){
        if(ctx.performed){
            if(!(canJump&& !playerState.isMelted)){//&& JumpTime > 0 
                return;
            } 
            DoJump();
        }
    }
    private void Awake() {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<CollisionState>(out collisionState);
        TryGetComponent<StatusSystem>(out playerState);
        
    }
    private void FixedUpdate() {
        if(isJumping){
            float t = (Time.fixedTime - jumpStartTime) / jumpDuration;
            if(collisionState.touchingCeiling) {
                // Cancel jump
                jumpStartTime = 0;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else rb.velocity = new Vector2(rb.velocity.x, jumpCurve.Evaluate(t)*20f);
        }
        if(collisionState.grounded) JumpTime = 1;
    }
}
