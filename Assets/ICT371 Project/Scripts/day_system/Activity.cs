using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Activity : MonoBehaviour
{
    public UnityEvent onStart;
    public UnityEvent onEnd;
    public UnityEvent onWin;
    public UnityEvent onLose;

    public bool start()
    {
        return false;
    }

    public void cancel()
    {
        
    }
}
