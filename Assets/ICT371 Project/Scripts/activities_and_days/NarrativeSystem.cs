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

    private static NarrativeSystem _instance;

    // Narrative properties
    // -----------------------------------------------------------------
    int _currentDayIndex = 0;
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