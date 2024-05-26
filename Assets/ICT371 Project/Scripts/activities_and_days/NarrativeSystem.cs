using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using JetBrains.Annotations;
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
    
    public enum CognitiveStage
    {
        Early,
        Middle,
        Late,
        Deceased
    }

    private static NarrativeSystem _instance;

    // Narrative properties
    // -----------------------------------------------------------------
    int _currentDayIndex = 0;
    // -----------------------------------------------------------------

    // Cognitive decay modifiers
    // -----------------------------------------------------------------
    private const double k_MinDecay = 0.0d, k_MaxDecay = 1.0d;
    private double _decay = 0.0d;
    private double _decayRate = -0.8d;
    private CognitiveStage _stage = CognitiveStage.Early;
    // -----------------------------------------------------------------

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        foreach (Day day in _days)
        {
            day.OnDayEnd.AddListener(EndDay);
        }
    }

    public static NarrativeSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NarrativeSystem>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(NarrativeSystem).Name);
                    _instance = singleton.AddComponent<NarrativeSystem>();
                    DontDestroyOnLoad(singleton);
                }
            }

            return _instance;
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

        foreach (IActivity activity in activities)
        {
            // book can only handle 3 tasks for now
            if (activities.IndexOf(activity) >= 3)
            {
                break;
            }
            
            // store current index for callback
            int currentIndex = activities.IndexOf(activity);

            // update books 'tasks' with activity names
            _bookInterface.SetTaskText(currentIndex + 1, activity.ActivityName);

            // bind activity completion to book interface
            activity.OnEnd.AddListener(() =>
            {
                _bookInterface.SetTaskCompletion(currentIndex + 1, activity.IsWon);
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
    }

    public void EndDay()
    {
        if (_currentDayIndex >= _days.Count)
        {
            return;
        }

        _days[_currentDayIndex++].EndDay();
        
        EvaluateDecay();
        EvaluateStage();

        _onDayEnd.Invoke();
    }

    public double GetCognitiveDecay()
    {
        return _decay;
    }

    public CognitiveStage GetCognitiveStage()
    {
        return _stage;
    }

    public Day CurrentDay => _days[_currentDayIndex];

    public int CurrentDayNum => _currentDayIndex + 1;

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