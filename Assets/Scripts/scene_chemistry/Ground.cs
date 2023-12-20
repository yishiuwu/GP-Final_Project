using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool win = false;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        win = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == "Ping"){
            Destroy(other.gameObject);
            if(!win){
                // WinCount(1.0f, ()=>{
                    cam.GetComponent<CamController>().WinCam();
                // });
                win = true;
            }
        }
    }

    IEnumerator WinCount( float duration, System.Action callback) {
        float startTime = Time.time;
        float t = (Time.time - startTime) / duration;
        while (t < 1) {
            yield return null;
            t = (Time.time - startTime)/duration;
        }
        callback?.Invoke();
    }
}
