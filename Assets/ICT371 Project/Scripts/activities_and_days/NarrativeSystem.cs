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
    
    [SerializeField] 
    List<Day> _days;

    [SerializeField] 
    BookInterface _bookInterface;

    // [SerializeField] PlayerStatus _playerInterface;

    private static NarrativeSystem _instance;
    int _currentDayIndex = 0;

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
            day.OnDayEnd.AddListener(OnDayEnd);
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

        // start the current day
        _days[_currentDayIndex].StartDay();

        // invoke the day start event for the narrative
        _onDayStart.Invoke();
    }

    public void OnDayEnd()
    {
        _onDayEnd.Invoke();
        // _playerInterface.EvaluateDecay(_currentDayIndex);
        // _playerInterface.EvaluateStage();
    }

    public void StartNextDay()
    {
        if (_currentDayIndex >= _days.Count)
        {
            return;
        }

        _currentDayIndex++;
        StartDay();
    }

    public int CurrentDayNumber => _currentDayIndex + 1;

    public Day CurrentDay => _days[_currentDayIndex];
}