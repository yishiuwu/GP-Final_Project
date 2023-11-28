using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ping : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isMoving){
            transform.Translate(moveSpeed * -1 * Time.deltaTime, 0, 0);
        }
    }

    IEnumerator StartMoveTimeCounter(float duration, System.Action callback = null){
        Debug.Log("start move ping");

        float t = 0;
        float startTime = Time.time;

        while(t < 1){
            isMoving = true;
            // image.color = Color.Lerp(origin, to, t);
            yield return null;
            t = (Time.time - startTime) / duration;
            // Debug.Log(t);
        }
        // image.color = origin;
        callback?.Invoke();
    }

    public void Move(){
        Debug.Log("call Move");
        Debug.Log(isMoving);
        if(!isMoving){
            Debug.Log("start move time counter");
            StartCoroutine(StartMoveTimeCounter(0.2f, ()=>{isMoving = false;}));
        }
    }
}
