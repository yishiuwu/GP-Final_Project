using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageSelect : MonoBehaviour
{
    StageNode currentNode;
    [SerializeField] GameObject catAvt;  // the cat avater stop on stage
    [SerializeField] AnimationCurve moveCurve;
    [SerializeField] float moveDur = 1;
    bool isMoving;  // is moving from node to node
    readonly Vector3 avtOffset = new Vector3(0, 0.2f);
    // Start is called before the first frame update
    void Start()
    {
        if (!currentNode) {
            currentNode = GetComponentInChildren<StageNode>();
            // Debug.Log(currentNode.gameObject.name);
        }
        // catAvt = GetComponentInChildren<CatAvater>().gameObject;
        catAvt.transform.position = currentNode.transform.position + avtOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenStage(string stageName) {
        GameManager.sceneTransition.ChangeScene(stageName);
    }

    public void Enter(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            isMoving = true;
            currentNode.Invoke();
        }
    }

    public void MoveUp(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed && !isMoving) {
            // Debug.Log("move up");
            isMoving = true;
            StartCoroutine(Move(currentNode, currentNode.navigation.upNode));
        }
    }
    public void MoveDown(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed && !isMoving) {
            // Debug.Log("move down");
            isMoving = true;
            StartCoroutine(Move(currentNode, currentNode.navigation.downNode));
        }
    }
    public void MoveLeft(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed && !isMoving) {
            // Debug.Log("move left");
            isMoving = true;
            StartCoroutine(Move(currentNode, currentNode.navigation.leftNode));
        }
    }
    public void MoveRight(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed && !isMoving) {
            // Debug.Log("move right");
            isMoving = true;
            StartCoroutine(Move(currentNode, currentNode.navigation.rightNode));
        }
    }

    // move from "from" node to "to" node
    IEnumerator Move(StageNode from, StageNode to) {
        if (to == null || to.isLock == true) {
            isMoving = false;
            yield break;
        }
            
        Vector3 targetPos = to.transform.position + avtOffset;
        Vector3 currentPos = from.transform.position + avtOffset;
        float t = 0, startTime = Time.time;
        while (t < 1) {
            catAvt.transform.position = Vector3.Lerp(currentPos, targetPos, moveCurve.Evaluate(t));
            // Debug.Log(catAvt.transform.position.ToString());
            yield return null;
            t = (Time.time-startTime)/moveDur;
        }

        // yield return new WaitForSeconds(0.5f);
        currentNode = to;
    
        isMoving = false;
        // Debug.Log(currentNode.name);
        yield return null;
    }

}
