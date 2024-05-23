using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Author: Marco Garzon Lara
// Author: Lane O'Rafferty
public class Day
{
    public List<Activity> activities;
    public UnityEvent onStart;
    public UnityEvent onEnd;

    Activity _currentActivity;

    public Day()
    {
        activities = new List<Activity>();
        onStart = new UnityEvent();
        onEnd = new UnityEvent();
    }

    public void StartDay()
    {
        onStart.Invoke();
        _currentActivity = activities[0];
        _currentActivity.onEnd.AddListener(AdvanceActivity);
        _currentActivity.StartActivity();
    }

    private void AdvanceActivity()
    {
        int index = activities.IndexOf(_currentActivity);
        
        if (index < activities.Count - 1)
        {
            _currentActivity.EndActivity();
            _currentActivity = activities[index + 1];
            _currentActivity.onEnd.AddListener(AdvanceActivity);
            _currentActivity.StartActivity();
        }
        else
        {
            onEnd.Invoke();
        }
    }
}
