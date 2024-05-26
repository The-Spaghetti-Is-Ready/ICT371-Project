using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.iOS;

public class NarrativeSystem : MonoBehaviour
{
    public enum CognitiveStage
    {
        EARLY,
        MIDDLE,
        LATE,
        DECEASED
    }

    [SerializeField] List<Day> _days;

    [SerializeField] BookInterface _bookInterface;

    int _currentDay = 0;

    // Cognitive decay modifiers
    private const double k_MinDecay = 0.0d, k_MaxDecay = 1.0d;
    private double _decay = 0.0d;
    private double _decayRate = -0.8d;
    private CognitiveStage _stage = CognitiveStage.EARLY;

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
            activities[i].OnEnd.AddListener(() => { _bookInterface.SetTaskCompletion(i + 1, activities[i].IsWon); });
        }

        // start the current day and increment the day counter
        _days[_currentDay++].StartDay();
    }

    public void EndDay()
    {
        // Evaluate the (daily) cognitive decay s.t. f(x) = c exp(kx)
        // TODO: Add modifiers into decay rate
        _decay = k_MaxDecay * Math.Exp(_decayRate * _currentDay);

        if (_decay > 0.3d)
        {
            _stage = CognitiveStage.MIDDLE;
        }

        if (_decay > 0.6d)
        {
            _stage = CognitiveStage.LATE;
        }

        if (_decay == k_MaxDecay)
        {
            _stage = CognitiveStage.DECEASED;
        }

        _days[_currentDay].EndDay();
    }

    public double GetCognitiveDecay()
    {
        return _decay;
    }

    public CognitiveStage GetCognitiveStage()
    {
        return _stage;
    }
}