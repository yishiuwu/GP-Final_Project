using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : ScriptAnimator
{
    // private Runner runner;
    // private Jumper jumper;
    private Player player;
    [SerializeField] private StatusSystem playerState;
    Animator animator;

    // private FacingDirection facingDirection;
    // private Vector3 initialLocalScale;
    protected override void Init()
    {
        #if UNITY_EDITOR
        RequireStates(
            nameof(AnimState.Idle),
            nameof(AnimState.Jump),
            nameof(AnimState.Fall),
            nameof(AnimState.Run),
            nameof(AnimState.Victory)
        );
        #endif
        TryGetComponent<Player>(out player);
        // TryGetComponent<StatusSystem>(out playerState);
        animator = GetComponent<Animator>();
    }
    // void Start()
    // {
       
    // }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning" , playerState.isRunning);
        if(playerState.isWin){
            // PlayStateIfNotInState(AnimState.Victory);
        }
        // else if(jumper.isRising){
        //     PlayStateIfNotInState(AnimState.Jump);
        // }
        // else if(jumper.isFalling){
        //     PlayStateIfNotInState(AnimState.Fall);
        // }
        // else if(playerState.isRunning){
        //     PlayStateIfNotInState(AnimState.Run);
        // }
        else if(playerState.isMelted){
            PlayStateIfNotInState(AnimState.Melt);
        }
        // else{
        //     PlayStateIfNotInState(AnimState.Idle);
        // }
        // if(facingDirection.isFacingAwayFromInitialDirection) transform.localScale 
        // = new Vector3(-initialLocalScale.x, initialLocalScale.y, initialLocalScale.z);
        // else transform.localScale = initialLocalScale;
    }
}
