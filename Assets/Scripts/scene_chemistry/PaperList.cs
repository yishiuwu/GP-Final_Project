using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperList : MonoBehaviour
{
    public Paper[] paperList;

    public string[] ansList = new string[]
    {
        "alkali",
        "acid",
        "alkali",
        "acid",
        "acid"
    };

    bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!win){
            if(CheckAnswer()){
                win = true;
                Debug.Log("win");
                GameManager.currentStage.Win();
            }
        }
    }

    bool CheckAnswer(){
        for(int i = 0; i < paperList.Length; i++){
            if(paperList[i].ph != ansList[i])
                return false;
        }
        return true;
    }
}
