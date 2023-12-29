using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public ParticleSystem tipsParticleSystem;
    public int m_sec;
    // Start is called before the first frame update
    void Start()
    {
        tipsParticleSystem = GetComponent<ParticleSystem>();
        tipsParticleSystem.Pause();
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        while(m_sec > 0)
        {
            Debug.Log($"time: {m_sec}");
            yield return new WaitForSeconds(1);
            m_sec--;
            if(m_sec<0)
            {
                m_sec = 0;
            }
        }
        tipsParticleSystem.Play();
    }
}
