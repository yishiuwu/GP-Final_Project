using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioSystem : MonoBehaviour
{
    private AudioSource audioSource;
    public float volume;
    [SerializeField] Slider slider;
    [SerializeField] string key = "music volume";
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DataManager.Load(key, 0.8f, out volume);
        slider.SetValueWithoutNotify(volume);
    }

    public void OnSceneChange() {
        DataManager.Set(key, volume);
    }

    public void StopBgm() {
        audioSource.Stop();
    }

    public void PlayBgm() {
        audioSource.Play();
    }

    public void SetBgm(AudioClip bgm) {
        audioSource.clip = bgm;
    }

    public void PlaySE(AudioClip se) {
        audioSource.PlayOneShot(se);
    }

    public void SetMusicVolume(float vol) {
        Debug.Log(vol);
        volume = vol;
        audioSource.volume = vol;
    }

}
