using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TransitionEffect : MonoBehaviour
{
    [SerializeField]
    protected float duration = 1.0f;
    protected Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        // if (image) Debug.Log("get image success");
        // else Debug.Log("get image fail");
    }

}
