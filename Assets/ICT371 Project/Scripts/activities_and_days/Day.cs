using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Author: Marco Garzon Lara
// Author: Lane O'Rafferty
[System.Serializable]
public class Day : MonoBehaviour
{   
    public UnityEvent OnDayEnd;

    [SerializeField]
    List<MonoBehaviour> _activities;
    List<IActivity> _activityList;
    int _currentActivityIndex = 0;
    int _activitesCompleted = 0;

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
        _activityList[_currentActivityIndex].StartActivity();
    }

    public void EndDay()
    {
        ActivityList.ForEach(activity => activity.EndActivity());
    }

    public void EndCurrentActivity()
    {
        _activityList[_currentActivityIndex].EndActivity();
    }

    public bool IsDayComplete()
    {
        return _currentActivityIndex >= _activityList.Count;
    }
    public IActivity CurrentActivity { get => _activityList[_currentActivityIndex]; }

    public List<IActivity> ActivityList { get => _activityList; }

    public int ActivitiesCompleted { get => _activitesCompleted; }

    void AdvanceActivity()
    {
        _activityList[_currentActivityIndex++].EndActivity();
        _activitesCompleted++;    

        if (_currentActivityIndex < _activityList.Count)
        {
            _activityList[_currentActivityIndex].StartActivity();
        }
        else
        {
            OnDayEnd.Invoke();
        }
    }
}
