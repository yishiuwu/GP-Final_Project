using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMotion : MonoBehaviour
{
    [SerializeField] AnimationCurve aMoveCurve;
    [SerializeField] AnimationCurve iMoveCurve;
    [SerializeField] Vector3 inPos;
    [SerializeField] Vector3 acPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActiveMove(Vector3 from, Vector3 to, float dur) {
        float t = 0, startTime = Time.time;
        while (t<1) {
            transform.position = Vector3.Lerp(from, to, aMoveCurve.Evaluate(t));
            yield return null;
            t = (Time.time - startTime)/dur;
        }
    }

    IEnumerator InactiveMove(Vector3 from, Vector3 to, float dur) {
        float t = 0, startTime = Time.time;
        while (t<1) {
            transform.position = Vector3.Lerp(from, to, iMoveCurve.Evaluate(t));
            yield return null;
            t = (Time.time - startTime)/dur;
        }
    }
}
