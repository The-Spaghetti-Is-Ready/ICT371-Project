using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Day : MonoBehaviour
{
    private List<Activity> activities;
    
    public UnityEvent onStart;
    public UnityEvent onEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
