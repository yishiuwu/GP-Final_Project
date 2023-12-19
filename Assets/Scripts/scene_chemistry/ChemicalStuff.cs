using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalStuff : MonoBehaviour
{
    private bool touchingCat;
    private GameObject cat;

    public string ph = "neutral";

    public float num = 10.0f;
    public float decayTime = 1.0f;
    public float norm = 5.0f;

    private Vector3 scaleChange;

    private bool startDye = false;

    // Start is called before the first frame update
    void Start()
    {
        touchingCat = false;
        cat = null;
        scaleChange = new Vector3(transform.localScale.x / num, transform.localScale.y / num, transform.localScale.z / num);
        // color = gameObject.color;
        // Debug.Log(color);
        startDye = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if(touchingCat && cat != null && StatusSystem.Instance.isMelted){
        //     Debug.Log("pui");
        //     StartCoroutine(StartDyingCat(() => {
        //         touchingCat = false;
        //         cat = null;
        //     }));
        // }
    }

    // bug? 碰到後變state 若換game object不會有問題
    void OnTriggerEnter2D(Collider2D other){
        // Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player" /*|| other.gameObject.tag == "MeltPlayer" && StatusSystem.isMelted */){
            // Debug.Log("get player");
            // Debug.Log(other.gameObject.tag);
            touchingCat = true;
            cat = other.gameObject;
            StartCoroutine(StartDyingCat(() => {
                touchingCat = false;
                cat = null;
            }));
        }
    }

    void OnTriggerExit2D(Collider2D other){
        // Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "MeltPlayer"){
            Debug.Log("player go");
            Debug.Log(other.gameObject.tag);
            touchingCat = false;
            cat = null;
        }
    }

    IEnumerator StartDyingCat(System.Action callback = null){
        Debug.Log("start dying cat");

        while(touchingCat && cat != null  /*&& StatusSystem.Instance.isMelted*/ ){
            num -= 1;
            // Debug.Log("cost one");

            transform.localScale -= scaleChange;
            if(ph == "acid")
                StatusSystem.Instance.Ph2Ac();
            else if(ph == "alkali")
                StatusSystem.Instance.Ph2Al();

            if(num <= 0){
                touchingCat = false;
                cat = null;
                // Debug.Log("no stuff left");
                Destroy(gameObject);
                callback?.Invoke();
            }
            yield return new WaitForSeconds(decayTime);
        }
    }

}
