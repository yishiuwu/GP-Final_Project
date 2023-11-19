using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSystem : MonoBehaviour
{
    private static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void StopBgm() {
        audioSource.Stop();
    }

    public static void PlayBgm() {
        audioSource.Play();
    }

    public static void SetBgm(AudioClip bgm) {
        audioSource.clip = bgm;
    }

    public static void PlaySE(AudioClip se) {
        audioSource.PlayOneShot(se);
    }

}
