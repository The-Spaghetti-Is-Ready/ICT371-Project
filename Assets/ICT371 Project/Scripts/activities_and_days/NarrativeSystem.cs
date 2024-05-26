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

    int _currentDayIndex = 0;

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
        // bounds check
        if (_currentDayIndex >= _days.Count)
        {
            return;
        }

        // get activities for current day
        var activities = _days[_currentDayIndex].ActivityList;

        // setup book callbacks
        for (int i = 0; (i < activities.Count) && (i < 3); i++)
        {
            
            // update books 'tasks' with activity names
            _bookInterface.SetTaskText(i + 1, activities[i].ActivityName);

            // store current index for callback
            int currentIndex = i;

            // bind activity completion to book interface
            activities[i].OnEnd.AddListener(() =>
            {
                _bookInterface.SetTaskCompletion(currentIndex + 1, activities[currentIndex].IsWon);
            });
        }

        // reset task completion status
        _bookInterface.ResetTasks();

        // start the current day and increment the day counter
        _days[_currentDayIndex++].StartDay();
    }
}
