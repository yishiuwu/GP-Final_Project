using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    private SpriteRenderer sp;
    public float fadeSpeed = 1.0f;

    private Color acidColor;
    private Color alkaliColor;
    private Color neutralColor;

    public string ph = "neutral";

    private bool changingColor = false;
    private bool getCat = false;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        acidColor = new Color(0.9411765f, 0.7294118f, 0.6627451f);
        alkaliColor = new Color(0.6627451f, 0.7529412f, 0.9764706f);
        neutralColor = Color.white;

        switch(ph){
            case "neutral":
                sp.color = neutralColor;
                break;
            case "acid":
                sp.color = acidColor;
                break;
            case "alkali":
                sp.color = alkaliColor;
                break;
        }

        changingColor = false;
        getCat = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(/*other.gameObject.tag == "Player" &&*/ other.gameObject.tag == "bone" && StatusSystem.Instance.isMelted && !changingColor){
            // Debug.Log("get Player");
            // Debug.Log(ph);
            // Debug.Log(StatusSystem.Instance.ph);
            getCat = true;

            if(StatusSystem.Instance.ph == "acid"){
                Debug.Log("go acid");
                if(ph == "neutral"){
                    changingColor = true;
                    StartCoroutine(Fade(acidColor, fadeSpeed, ()=>{
                        changingColor = false;
                        ph = "acid";
                    }));
                }
                else if(ph == "alkali"){
                    changingColor = true;
                    StartCoroutine(Fade(neutralColor, fadeSpeed, ()=>{
                        StartCoroutine(Fade(acidColor, fadeSpeed, ()=>{
                            changingColor=false;
                            ph = "acid";
                        }));
                    }));
                }
            }
            else if(StatusSystem.Instance.ph == "alkali"){
                Debug.Log("go alkali");
                if(ph == "neutral"){
                    changingColor = true;
                    StartCoroutine(Fade(alkaliColor, fadeSpeed, ()=>{
                        changingColor = false;
                        ph = "alkali";
                    }));
                }
                else if(ph == "acid"){
                    changingColor = true;
                    StartCoroutine(Fade(neutralColor, fadeSpeed, ()=>{
                        StartCoroutine(Fade(alkaliColor, fadeSpeed, ()=>{
                            changingColor=false;
                            ph = "alkali";
                        }));
                    }));
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(/*other.gameObject.tag == "Player"*/ other.gameObject.tag == "bone"){
            getCat = false;
        }
    }

    IEnumerator Fade(Color to, float duration, System.Action callback) {
        float startTime = Time.time;
        float t = (Time.time - startTime) / duration;
        Color from = sp.color;
        // Debug.Log(t);
        while (t < 1) {
            sp.color = Color.Lerp(from, to, t);
            yield return null;
            t = (Time.time - startTime)/duration;
            // Debug.Log(t);
        }
        callback?.Invoke();
    }
}
