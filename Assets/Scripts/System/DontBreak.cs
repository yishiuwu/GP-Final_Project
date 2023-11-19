using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontBreak : MonoBehaviour
{
    void Awake()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if (targets.Length > 1) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
