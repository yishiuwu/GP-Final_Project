using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalStuff : MonoBehaviour
{
    private Color color;
    private bool touchingCat;
    private GameObject cat;

    public float num = 10.0f;
    public float decayTime = 1.0f;
    public float norm = 5.0f;

    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        touchingCat = false;
        cat = null;
        scaleChange = new Vector3(transform.localScale.x / num, transform.localScale.y / num, transform.localScale.z / num);
        // color = gameObject.color;
        Debug.Log(color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // bug? 碰到後變state 若換game object不會有問題
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            touchingCat = true;
            cat = other.gameObject;
            StartCoroutine(StartDyingCat(() => {
                touchingCat = false;
                cat = null;
            }));
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            touchingCat = false;
            cat = null;
        }
    }

    IEnumerator StartDyingCat(System.Action callback = null){
        Debug.Log("start dying cat");

        while(touchingCat && cat != null){
            num -= 1;
            Debug.Log("cost one");

            transform.localScale -= scaleChange;
            // cat.GetComponent<Renderer>().color = color;

            if(num <= 0){
                touchingCat = false;
                cat = null;
                Debug.Log("no stuff left");
                Destroy(gameObject);
                callback?.Invoke();
            }
            yield return new WaitForSeconds(decayTime);
        }
    }

}
