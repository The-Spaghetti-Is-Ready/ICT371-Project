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
    int _activitesCompleted = 0;

    void Awake()
    {
        _activityList = new List<IActivity>();
        foreach (MonoBehaviour behaviour in _activities)
        {
            if (behaviour is IActivity activity)
            {
                _activityList.Add(activity);
                activity.OnEnd.AddListener(OnActivityComplete);
            }
        }
    }

    public void StartDay()
    {
         _activityList.ForEach(activity => activity.StartActivity());
    }

    public void EndDay()
    {
        _activityList.ForEach(activity => activity.EndActivity());
    }

    public List<IActivity> ActivityList { get => _activityList; }

    public int ActivitiesCompleted { get => _activitesCompleted; }

    void OnActivityComplete()
    {
        if (_activitesCompleted < _activityList.Count)
        {
            _activitesCompleted++;
        }

        if (_activitesCompleted == _activityList.Count)
        {
            OnDayEnd.Invoke();
        }
    }
}
