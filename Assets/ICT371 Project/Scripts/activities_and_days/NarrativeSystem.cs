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
        Early,
        Middle,
        Late,
        Deceased
    }

    [SerializeField] List<Day> _days;

    [SerializeField] BookInterface _bookInterface;

    int _currentDayIndex = 0;

    // Cognitive decay modifiers
    private const double k_MinDecay = 0.0d, k_MaxDecay = 1.0d;
    private double _decay = 0.0d;
    private double _decayRate = -0.8d;
    private CognitiveStage _stage = CognitiveStage.Early;

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

    public void EndDay()
    {
        EvaluateDecay();
        EvaluateStage();

        _days[_currentDayIndex].EndDay();
    }

    public double GetCognitiveDecay()
    {
        return _decay;
    }

    public CognitiveStage GetCognitiveStage()
    {
        return _stage;
    }

    private void EvaluateDecay()
    {
        // TODO: Add modifiers into decay rate
        // Evaluate the (daily) cognitive decay s.t. f(x) = c exp(kx)
        _decay = k_MaxDecay * Math.Exp(_decayRate * _currentDayIndex);
        Debug.Log("Decay: " + _decay + ", Day: " + _currentDayIndex);
    }

    private void EvaluateStage()
    {
        if (_decay > 0.3d)
        {
            _stage = CognitiveStage.Middle;
        }

        if (_decay > 0.6d)
        {
            _stage = CognitiveStage.Late;
        }

        if (_decay > k_MaxDecay)
        {
            _stage = CognitiveStage.Deceased;
        }

        Debug.Log("Stage: " + _stage);
    }
}