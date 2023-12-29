using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorEffect : TransitionEffect
{
    private Color fadeColor;

    void Start()
    {
        // SetColor(Color.white);
        // Debug.Log(image.ToString());
    }

    IEnumerator Fade(Color to, float duration, System.Action callback) {
        float startTime = Time.time;
        float t = (Time.time - startTime)/duration;
        Color from = image.color;
        // Debug.Log(t);
        while (t < 1) {
            image.color = Color.Lerp(from, to, t);
            yield return null;
            t = (Time.time - startTime)/duration;
            // Debug.Log(t);
        }
        callback?.Invoke();
    }
    public void SetFadeColor(Color color) {
        fadeColor = color;
    }
    public void SetFadeDuration(float d) {
        duration = d;
    }
    public void SetColor(Color color) {
        image.color = color;
    }
    public void StartFade(Color to, float duration, System.Action callback = null) {
        // Debug.Log("start fade");
        StartCoroutine(Fade(to, duration, callback));
    }
    public void StartFadeWithNoCallback() {
        // Debug.Log("start fade");
        StartCoroutine(Fade(fadeColor, duration, null));
    }
}
