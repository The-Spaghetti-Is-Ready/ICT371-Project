using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Author: Marco Garzon Lara
// Author: Lane O'Rafferty
public class Day : MonoBehaviour
{
    public List<IActivity> activities;
    public UnityEvent onStart;
    public UnityEvent onEnd;

    IActivity _currentActivity;

    public void StartDay()
    {
        onStart.Invoke();
        _currentActivity = activities[0];
        _currentActivity.OnEnd.AddListener(AdvanceActivity);
        _currentActivity.StartActivity();
    }

    public void EndCurrentActivity()
    {
        _currentActivity.EndActivity();
    }

    void AdvanceActivity()
    {
        int index = activities.IndexOf(_currentActivity);

        if (index < activities.Count - 1)
        {
            _currentActivity.EndActivity();
            _currentActivity = activities[index + 1];
            _currentActivity.OnEnd.AddListener(AdvanceActivity);
            _currentActivity.StartActivity();
        }
        else
        {
            onEnd.Invoke();
        }
    }
}
