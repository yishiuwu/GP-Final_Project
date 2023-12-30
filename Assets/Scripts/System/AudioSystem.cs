using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioSystem : MonoBehaviour
{
    private AudioSource audioSource;
    public float volume;
    public bool isMute = false;
    [SpaceAttribute]
    [SerializeField] Slider slider;
    [SerializeField] Image image;
    [SerializeField] string key = "music";
    [SpaceAttribute]
    [SerializeField] Sprite unmuteImg;
    [SerializeField] Sprite muteImg;

    // Start is called before the first frame update
    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        DataManager.Load(key+DataManager.volumeKey, 0.8f, out volume);
        DataManager.Load(key+DataManager.ismutedKey, 0, out int mute);
        isMute = mute == 1;
        slider.value = volume;
        image.sprite = isMute ? muteImg : unmuteImg;
        PlayBgm();
    }

    public void OnSceneChange() {
        // Debug.Log("save volume");
        StopBgm();
        int mute = isMute?1:0;
        DataManager.Set(key+DataManager.volumeKey, volume);
        DataManager.Set(key+DataManager.ismutedKey, mute);
    }

    public void StopBgm() {
        Debug.Log("stop bgm");
        StartCoroutine(FadeOut(0.8f));
        // FadeOut(0.2f);
    }

    public void PlayBgm() {
        Debug.Log("play bgm");
        StartCoroutine(FadeIn(0.8f));
    }

    public void SetBgm(AudioClip bgm) {
        audioSource.clip = bgm;
    }

    public void PlaySE(AudioClip se) {
        audioSource.PlayOneShot(se);
    }

    public void ToggleMute() {
        isMute = !isMute;
        audioSource.mute = isMute;
        image.sprite = isMute ? muteImg : unmuteImg;
    }

    public void SetMusicVolume(float vol) {
        // Debug.Log(vol);
        volume = vol;
        audioSource.volume = vol;
    }

    IEnumerator FadeIn(float duration) {
        float t = 0, startTime = Time.time;
        audioSource.volume = 0;
        audioSource.Play();
        while (t < 1) {
            audioSource.volume = Mathf.Lerp(0, volume, t);
            // Debug.Log(audioSource.volume);
            yield return null;
            t = (Time.time - startTime)/duration;
        }
        yield break;
    }
    IEnumerator FadeOut(float duration) {
        float t = 0, startTime = Time.time;
        while (t < 1) {
            audioSource.volume = Mathf.Lerp(volume, 0, t);
            yield return null;
            t = (Time.time - startTime)/duration;
        }
        audioSource.Stop();
        yield break;
    }

}
