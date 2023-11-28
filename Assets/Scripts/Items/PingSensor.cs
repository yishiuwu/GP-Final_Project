using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingSensor : MonoBehaviour
{
    public GameObject Ping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            // Ping.transform.Translate(Vector2.left*10*Time.deltaTime,0,0);
            Debug.Log("get player");
            Ping.gameObject.GetComponent<Ping>().Move();
        }
    }
}
