using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsCtrl : MonoBehaviour
{
    public GameObject Battery;
    private bool noEnergy => (Battery.GetComponent<BatteryCtrl>().EnergyEmpty);
    private bool EnergyFull => (Battery.GetComponent<BatteryCtrl>().EnergyFull);
    public bool lightOn = false;
    public Sprite normalSprite; // 普通状态的图片
    public Sprite litSprite;    // 亮起状态的图片
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(EnergyFull) lightOn = true;
        if(noEnergy) lightOn = false;

        if(lightOn){
            spriteRenderer.sprite = litSprite;
            spriteRenderer.sortingOrder = 5;
        }else{
            spriteRenderer.sprite = normalSprite;
            spriteRenderer.sortingOrder = 5;
        }
    }
}
