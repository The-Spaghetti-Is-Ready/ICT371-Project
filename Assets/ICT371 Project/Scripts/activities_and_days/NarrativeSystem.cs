using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.iOS;
using UnityEngine.XR.Interaction.Toolkit;

public class NarrativeSystem : MonoBehaviour
{
    [SerializeField]
    UnityEvent _onDayStart;
    
    [SerializeField]
    UnityEvent _onDayEnd;
    
    [SerializeField] 
    List<Day> _days;

    [SerializeField]
    List<GameObject> _nextDayActivators;

    [SerializeField]
    CameraFade _cameraFade;

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

        foreach (GameObject activator in _nextDayActivators)
        {
            var interactable = activator.AddComponent<XRSimpleInteractable>();
            interactable.activated.AddListener(StartNextDay);
            interactable.activated.AddListener((args) => { interactable.enabled = false; });
            interactable.enabled = false;
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
        
        // fade camera in
        _cameraFade.FadeIn(2.0f);

        // disable the next day activator for the previous day
        if (_currentDayIndex > 0)
        {
            _nextDayActivators[_currentDayIndex - 1].GetComponent<XRSimpleInteractable>().enabled = false;
        }

        // start the current day
        _days[_currentDayIndex].StartDay();

        // invoke the day start event for the narrative
        _onDayStart.Invoke();
    }

    public void OnDayEnd()
    {
        // activate the activator for the next day
        if (_currentDayIndex < _nextDayActivators.Count)
        {
            _nextDayActivators[_currentDayIndex].GetComponent<XRSimpleInteractable>().enabled = true;
        }

        _onDayEnd.Invoke();
    }

    public void StartNextDay(ActivateEventArgs args)
    {
        if (_currentDayIndex >= _days.Count)
        {
            return;
        }

        _currentDayIndex++;
        _cameraFade.FadeOut(2.0f);
        Invoke("StartDay", 2.5f);
    }

    public int CurrentDayNumber => _currentDayIndex + 1;

    public Day CurrentDay => _days[_currentDayIndex];
}