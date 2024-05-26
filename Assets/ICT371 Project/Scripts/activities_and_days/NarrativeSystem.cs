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
        linkDaysToSystem();

        processDay();
        
        var activities = days[0].ActivityList;

        for (int i = 0; i < activities.Count; i++)
        {
            bookInterface.SetTaskText(i + 1, activities[i].ActivityName);
        }
    }

    void linkDaysToSystem()
    {
        foreach (var day in days)
        {
            day.onEnd.AddListener(processDay);
        }
    }

    void processDay()
    {
        
    }
}
