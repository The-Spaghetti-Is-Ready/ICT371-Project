using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Author: Marco Garzon Lara
// Author: Lane O'Rafferty 
public interface IActivity
{
    UnityEvent OnStart { get; }
    UnityEvent OnEnd { get; }
    bool IsWon { get; }
    string ActivityName { get; }
    
    void StartActivity();

    void EndActivity();
}
