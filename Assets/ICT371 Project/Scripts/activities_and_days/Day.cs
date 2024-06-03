using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <Author>
/// Lane O'Rafferty
/// Marco Garzon Lara
/// </Author>
/// <summary>
/// This class models a day in the game.
/// </summary>
[System.Serializable]
public class Day : MonoBehaviour
{   
    /// <summary>
    /// The diary entry for the day
    /// </summary>
    [TextArea(15, 30)]
    public string diaryEntry = "";

    [SerializeField]
    List<MonoBehaviour> _activities;
    
    /// <summary>
    /// The list of activities for the day.
    /// </summary>
    List<IActivity> _activityList;
    int _activitesCompleted = 0;

    public UnityEvent OnDayStart;

    public UnityEvent OnDayEnd;

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

    /// <summary>
    /// Starts the day by starting all activities and invoking the OnDayStart event.
    /// </summary>
    public void StartDay()
    {
         _activityList.ForEach(activity => activity.StartActivity());
         OnDayStart.Invoke();
    }

    /// <summary>
    /// Ends the day by ending all activities.
    /// </summary>
    public void EndDay()
    {
        _activityList.ForEach(activity => activity.EndActivity());
    }

    /// <summary>
    /// Gets the list of activities for the day.
    /// </summary>
    public List<IActivity> ActivityList { get => _activityList; }

    /// <summary>
    /// Gets the number of activities completed for the day.
    /// </summary>
    public int ActivitiesCompleted { get => _activitesCompleted; }

    /// <summary>
    /// Called when an activity is completed.
    /// </summary>
    /// <remarks>
    /// Increments the number of activities completed and checks if all activities have been completed.
    /// </remarks>
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
