using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMovement : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float stayDur = 1.0f;
    [SerializeField] float ioDur = 1.0f;
    readonly Vector3 centerPosition = new Vector3(960, 600, 0);
    [SerializeField] bool isWin;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = centerPosition + Vector3.right * Screen.width;
        // Debug.Log(transform.position.ToString());
        // if (isWin) GameManager.currentStage.OnWin += ()=>{StartMove(()=>{GameManager.sceneTransition.ChangeScene("StageSelect");});};
        // else GameManager.currentStage.OnStart += ()=>{StartMove();};
    }

    public void StartMove(Action callback = null) {
        Debug.Log("move");
        StartCoroutine(MoveCard(callback));
    }

    public IEnumerator MoveCard(Action callback) {
        float startTime = Time.time;
        float t = (Time.time - startTime)/ioDur;
        // Debug.Log(t);
        // Debug.Log(from.ToString());
        Vector3 from = centerPosition + Vector3.right * Screen.width;
        Vector3 to = centerPosition;
        while (t < 1) {
            transform.position = Vector3.Lerp(from, to, curve.Evaluate(t));
            yield return null;
            t = (Time.time - startTime)/ioDur;
            // Debug.Log(t);
        }
        yield return new WaitForSeconds(stayDur);
        Debug.Log(t);
        from = centerPosition;
        to = centerPosition + Vector3.left * Screen.width; 
        startTime = Time.time;
        t = 0;
        while (t < 1) {
            transform.position = Vector3.Lerp(from, to, curve.Evaluate(t));
            yield return null;
            t = (Time.time - startTime)/ioDur;
            // Debug.Log(t);
        }
        callback?.Invoke();
        yield break;
    }

    
}
