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

    private string ph = "neutral";

    private bool changingColor = false;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        acidColor = new Color(0.7169812f, 0.2885962f, 0.2885962f);
        alkaliColor = new Color(0.2901961f, 0.4346832f, 0.7176471f);
        neutralColor = Color.white;

        sp.color = neutralColor;
        ph = "neutral";

        changingColor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" /* && StatusSystem.isMelted */ && !changingColor){
            Debug.Log("get Player");
            Debug.Log(ph);
            Debug.Log(StatusSystem.Instance.ph);
            if(StatusSystem.Instance.ph == "acid"){
                Debug.Log("go acid");
                if(ph == "neutral"){
                    changingColor = true;
                    StartCoroutine(Fade(acidColor, fadeSpeed, ()=>{changingColor=false;}));
                }
                else if(ph == "alkali"){
                    changingColor = true;
                    StartCoroutine(Fade(neutralColor, fadeSpeed, ()=>{
                        StartCoroutine(Fade(alkaliColor, fadeSpeed, ()=>{changingColor=false;}));
                    }));
                }
            }
            else if(StatusSystem.Instance.ph == "alkali"){
                Debug.Log("go alkali");
                if(ph == "neutral"){
                    changingColor = true;
                    StartCoroutine(Fade(alkaliColor, fadeSpeed, ()=>{changingColor=false;}));
                }
                else if(ph == "acid"){
                    changingColor = true;
                    StartCoroutine(Fade(neutralColor, fadeSpeed, ()=>{
                        StartCoroutine(Fade(acidColor, fadeSpeed, ()=>{changingColor=false;}));
                    }));
                }
            }
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
