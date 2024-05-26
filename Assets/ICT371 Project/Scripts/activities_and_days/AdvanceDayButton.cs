using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceDayButton : MonoBehaviour
{
    [SerializeField]
    NarrativeSystem _narrativeSystem;
    [SerializeField]
    Material _readyMaterial;
    [SerializeField]
    Material _notReadyMaterial;

    bool _isReady = false;

    void Awake()
    {
        gameObject.GetComponent<Renderer>().material = _notReadyMaterial;
    }

    public void AdvanceDay()
    {
        if (!_isReady)
        {
            return;
        }

        _narrativeSystem.StartDay();
        SetReady(false);
    }

    public void SetReady(bool ready)
    {
        _isReady = ready;

        if (_isReady)
        {
            gameObject.GetComponent<Renderer>().material = _readyMaterial;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = _notReadyMaterial;
        }
    }
}
