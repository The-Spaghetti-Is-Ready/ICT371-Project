using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.iOS;

public class NarrativeSystem : MonoBehaviour
{
    [SerializeField]
    List<Day> _days;

    [SerializeField]
    BookInterface _bookInterface;

    int _currentDay = 0;

    void Start()
    {
        LinkDaysToSystem();
        StartDay();
    }

    void LinkDaysToSystem()
    {
        foreach (var day in _days)
        {
            day.onEnd.AddListener(StartDay);
        }
    }

    void StartDay()
    {
        // get activities for current day
        var activities = _days[_currentDay].ActivityList;

        // update books 'tasks' with activity names
        for (int i = 0; i < activities.Count && i < 3; i++)
        {
            _bookInterface.SetTaskText(i + 1, activities[i].ActivityName);
        }

        // reset task completion status
        _bookInterface.ResetTasks();

        // bind activity completion to book interface
        for (int i = 0; i < activities.Count && i < 3; i++)
        {
            activities[i].OnEnd.AddListener(() =>
            {
                _bookInterface.SetTaskCompletion(i + 1, activities[i].IsWon);
            });
        }

        // start the current day and increment the day counter
        _days[_currentDay++].StartDay();
    }
}
