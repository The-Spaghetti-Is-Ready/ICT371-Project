using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.iOS;

public class NarrativeSystem : MonoBehaviour
{
    [SerializeField]
    UnityEvent _onDayStart;
    [SerializeField]
    UnityEvent _onDayEnd;
    [SerializeField] BookInterface _bookInterface;
    [SerializeField] List<Day> _days;

    [SerializeField] PlayerStatus _playerInterface;

    // Narrative properties
    // -----------------------------------------------------------------
    int _currentDayIndex = 0;
    // -----------------------------------------------------------------

    void Awake()
    {
        foreach (Day day in _days)
        {
            day.OnDayEnd.AddListener(EndDay);
        }
    }

    void Start()
    {
        StartDay();
    }

    public void StartDay()
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

        // start the current day
        _days[_currentDayIndex].StartDay();

        // update the book day number
        _bookInterface.SetDayNumber(_currentDayIndex + 1);

        // invoke the day start event for the narrative
        _onDayStart.Invoke();

        // update the book day number
        _bookInterface.SetDayNumber(_currentDayIndex + 1);

        // invoke the day start event for the narrative
        _onDayStart.Invoke();
    }

    public void EndDay()
    {
        if (_currentDayIndex >= _days.Count)
        {
            return;
        }

        _days[_currentDayIndex++].EndDay();
        
        _playerInterface.EvaluateDecay(_currentDayIndex);
        _playerInterface.EvaluateStage();

        _onDayEnd.Invoke();
    }
}