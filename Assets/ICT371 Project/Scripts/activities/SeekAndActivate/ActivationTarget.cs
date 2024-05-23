using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTarget : MonoBehaviour
{
    public bool IsActivated { get => isActivated; }
    bool isActivated = false;

    public void Activate()
    {
        isActivated = true;
    }
}
