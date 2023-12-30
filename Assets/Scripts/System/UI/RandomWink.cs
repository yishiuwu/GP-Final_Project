using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RandomWink : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RandomW());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RandomW() {
        while (true) {
            anim.SetBool("wink", true);
            yield return new WaitForSeconds(0.3f);
            anim.SetBool("wink", false);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 5));
        }
    }
}
