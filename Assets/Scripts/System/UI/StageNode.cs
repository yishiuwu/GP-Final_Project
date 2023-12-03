using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageNode : MonoBehaviour
{
    public StageNode upNode, downNode, leftNode, rightNode;
    public bool isLock = true;
    public UnityEvent OnInvoke;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
