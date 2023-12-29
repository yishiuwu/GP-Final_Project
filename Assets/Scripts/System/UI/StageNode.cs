using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageNode : MonoBehaviour
{
    [Serializable]
    public struct Navigation
    {
        public StageNode upNode, downNode, leftNode, rightNode;
    }
    public bool isLock = true;
    [SerializeField] int id;
    [SerializeField] public Navigation navigation;
    [SpaceAttribute]
    public UnityEvent OnInvoke;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.Set(DataManager.stageKey, 4);
        DataManager.Load(DataManager.stageKey, 1, out int openedStage);
        isLock = id > openedStage;
        if (isLock) {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            Color newColor = sprite.color;
            newColor.a = 150;
            sprite.color = newColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Invoke() {
        OnInvoke?.Invoke();
    }
}
