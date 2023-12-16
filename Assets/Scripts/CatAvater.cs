using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAvater : MonoBehaviour
{
    [SerializeField]
    AnimationCurve moveCurve;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FloatAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FloatAnim() {
        const float MovePx = 0.5f;
        const float MoveDur = 1f;
        // float moveSpeed = 0.05f;
        int moveDir = 1;
        yield return null;
        while (true) {
            // something here crash the editor :(
            Vector3 startPos = transform.localPosition;
            Vector3 endPos = transform.localPosition + new Vector3(0, MovePx*moveDir);
            float startTime = Time.time;
            float t = (Time.time - startTime)/MoveDur;
            while (t < 1) {
                transform.localPosition = Vector3.Lerp(startPos, endPos, moveCurve.Evaluate(t));
                yield return null;
                t = (Time.time - startTime)/MoveDur;
            }
            moveDir *= -1;
        }
    }

}
