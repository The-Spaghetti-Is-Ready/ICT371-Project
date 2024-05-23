using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Author: Marco Garzon Lara
// Author: Lane O'Rafferty 
public abstract class Activity
{
    public UnityEvent onStart;
    public UnityEvent onEnd;
    public UnityEvent onWin;
    public UnityEvent onLose;

    public abstract bool StartActivity();

    public abstract void EndActivity();
}
