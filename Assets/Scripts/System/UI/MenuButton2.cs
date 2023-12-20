using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // public bool defaultSelected = false;


    // private Button btn;
    // Start is called before the first frame update
    // void Start()
    // {
    //     // btn = GetComponent<Button>();
    // }
    private float fixedT = 0;
    private float animDur = 0.1f;
    [SerializeField] private UnityEvent OnClick;

    readonly Vector3 normalScale = Vector3.one, highlightedScale = new Vector3(1.3f, 1.3f, 1);

    public void OnPointerEnter(PointerEventData eventData) {
        // Debug.Log(fixedT);
        StopCoroutine("ScaleTransform");
        StartCoroutine(ScaleTransform(normalScale, highlightedScale, false));
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Debug.Log(fixedT);
        StopCoroutine("ScaleTransform");
        StartCoroutine(ScaleTransform(highlightedScale, normalScale, true));
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("btn click");
        OnClick?.Invoke();
    }

    IEnumerator ScaleTransform (Vector3 startScale, Vector3 endScale, bool reverse = false) {
        float fixedStart = reverse ? Time.time - (1-fixedT) : Time.time - fixedT;
        while (!reverse && fixedT < 1) {
            transform.localScale = Vector3.Lerp(startScale, endScale, fixedT);
            yield return null;
            fixedT = (Time.time-fixedStart)/animDur;
        }
        while (reverse && fixedT > 0) {
            transform.localScale = Vector3.Lerp(startScale, endScale, 1-fixedT);
            yield return null;
            fixedT = 1-(Time.time-fixedStart)/animDur;
        }
        if (fixedT > 1) fixedT = 1;
        if (fixedT < 0) fixedT = 0;
        yield break;
    }
}
