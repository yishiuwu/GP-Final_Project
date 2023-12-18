using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCtrl : MonoBehaviour
{
    public GameObject Pipeline;
    private int Energy;
    private bool Charging  => (Pipeline.GetComponent<PipeLineCtrl>().Linked);
    private bool canAddSub = true;
    public bool EnergyFull = false;
    public bool EnergyEmpty = true;
    public AudioClip chargeSound;
    public AudioClip loseEnergySound;
    public AudioClip FullEnergySound;
    private AudioSource myAudioSource;
    void Start()
    {
        Energy = 0;
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Charging: {Charging}");
        if(Charging && !EnergyFull && canAddSub){
            ModifiEnergy(1);
            myAudioSource.PlayOneShot(chargeSound);
            StartCoroutine(WaitForAddSub());
        }else if(!Charging && !EnergyEmpty && canAddSub){
            ModifiEnergy(-1);
            myAudioSource.PlayOneShot(loseEnergySound);
            StartCoroutine(WaitForAddSub());
        }
    }
    void ModifiEnergy(int num){
        Energy += num;
        if(Energy >= 4){
          Energy = 4;  
          EnergyFull = true;
          myAudioSource.PlayOneShot(FullEnergySound);
        }else{
            EnergyFull = false;
        }
        if(Energy <= 0){
            Energy = 0;
            EnergyEmpty = true;
        }else{
            EnergyEmpty = false;
        }
        UpdateBettery();
    }
    void UpdateBettery(){
        for(int i=0; i<this.transform.childCount; i++){
            if(Energy > i) this.transform.GetChild(i).gameObject.SetActive(true);
            else this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    IEnumerator WaitForAddSub(){
        canAddSub = false;
        yield return new WaitForSeconds(2f);
        canAddSub = true;
    }
}
