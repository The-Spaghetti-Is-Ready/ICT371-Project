using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Author: Marco Garzon Lara
// Author: Lane O'Rafferty
public class Day : MonoBehaviour
{   
    public UnityEvent onStart;
    public UnityEvent onEnd;

    public IActivity CurrentActivity { get => _currentActivity; }

    public List<IActivity> ActivityList { get => _activityList; }
    
    [SerializeField]
    List<MonoBehaviour> _activities;
    List<IActivity> _activityList;
    IActivity _currentActivity;

    void Awake()
    {
        _activityList = new List<IActivity>();
        foreach (MonoBehaviour behaviour in _activities)
        {
            if (behaviour is IActivity activity)
            {
                _activityList.Add(activity);
                activity.OnEnd.AddListener(AdvanceActivity);
            }
        }
    }

    public void StartDay()
    {
        onStart.Invoke();
        _currentActivity = _activityList[0];
        _currentActivity.StartActivity();
    }

    public void EndDay()
    {
        onEnd.Invoke();
    }

    public void EndCurrentActivity()
    {
        _currentActivity.EndActivity();
    }

    void AdvanceActivity()
    {
        int index = _activityList.IndexOf(_currentActivity);

        if (index < _activityList.Count - 1)
        {
            _currentActivity.EndActivity();
            _currentActivity = _activityList[index + 1];
            _currentActivity.StartActivity();
        }
        else
        {
            onEnd.Invoke();
        }
    }
}
