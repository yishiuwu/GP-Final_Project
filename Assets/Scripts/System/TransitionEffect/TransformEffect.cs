using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformEffect : TransitionEffect
{
    [SerializeField] AnimationCurve moveCurve;
    Vector3 centerPosition;
    
    void Awake() {
        centerPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DownIn(Action callback = null) {
        Vector3 from = centerPosition + Vector3.up * Screen.height;
        Vector3 to = centerPosition; 
        StartCoroutine(MoveCard(from, to, duration, callback));
    }
    public void DownOut(Action callback = null) {
        Vector3 from = centerPosition;
        Vector3 to = centerPosition + Vector3.down * Screen.height; 
        StartCoroutine(MoveCard(from, to, duration, callback));
    }
    public void UpIn(Action callback = null) {
        Vector3 from = centerPosition + Vector3.down * Screen.height;
        Vector3 to = centerPosition; 
        StartCoroutine(MoveCard(from, to, duration, callback));
    }
    public void UpOut(Action callback = null) {
        Vector3 from = centerPosition;
        Vector3 to = centerPosition + Vector3.up * Screen.height; 
        StartCoroutine(MoveCard(from, to, duration, callback));
    }
    public void LeftIn(Action callback = null) {
        Vector3 from = centerPosition + Vector3.right * Screen.width;
        Vector3 to = centerPosition; 
        StartCoroutine(MoveCard(from, to, duration, callback));
    }
    public void LeftOut(Action callback = null) {
        Vector3 from = centerPosition;
        Vector3 to = centerPosition + Vector3.left * Screen.width; 
        StartCoroutine(MoveCard(from, to, duration, callback));
    }
    public void RightIn(Action callback = null) {
        Vector3 from = centerPosition + Vector3.left * Screen.width;
        Vector3 to = centerPosition; 
        StartCoroutine(MoveCard(from, to, duration, callback));
    }
    public void RightOut(Action callback = null) {
        Vector3 from = centerPosition;
        Vector3 to = centerPosition + Vector3.right * Screen.width; 
        StartCoroutine(MoveCard(from, to, duration, callback));
    }

    IEnumerator MoveCard(Vector3 from, Vector3 to, float duration, Action callback) {
        float startTime = Time.time;
        float t = (Time.time - startTime)/duration;
        
        Debug.Log(t);
        while (t < 1) {
            image.transform.position = Vector3.Lerp(from, to, moveCurve.Evaluate(t));
            yield return null;
            t = (Time.time - startTime)/duration;
            // Debug.Log(t);
        }
        callback?.Invoke();
    }
}
