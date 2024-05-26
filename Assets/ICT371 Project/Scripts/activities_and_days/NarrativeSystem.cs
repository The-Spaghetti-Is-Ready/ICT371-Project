using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class NarrativeSystem : MonoBehaviour
{
    [SerializeField]
    List<Day> days;

    [SerializeField]
    BookInterface bookInterface;

    int _currentDay = 0;

    void Start()
    {
        LinkDaysToSystem();
        StartDay();
    }

    void LinkDaysToSystem()
    {
        foreach (var day in days)
        {
            day.onEnd.AddListener(StartDay);
        }
    }

    void StartDay()
    {
        // get activities for current day
        var activities = days[_currentDay].ActivityList;

        // update books 'tasks' with activity names
        for (int i = 0; i < activities.Count; i++)
        {
            bookInterface.SetTaskText(i + 1, activities[i].ActivityName);
        }

        // start the current day and increment the day counter
        days[_currentDay++].StartDay();
    }
}
