using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeekAndActivate : MonoBehaviour, IActivity
{
    public UnityEvent OnStart { get => _onStart; }

    public UnityEvent OnEnd { get => _onEnd;}

    public bool IsWon { get => _isWon; }

    public string ActivityName { get => _activityName; }

    public int RemainingTime { get => _remainingTime; }

    [SerializeField]
    UnityEvent _onStart;
    [SerializeField]
    UnityEvent _onEnd;
    [SerializeField]
    int _timeLimitSeconds = 60;
    [SerializeField]
    string _activityName = "Seek and Activate";

    bool _isRunning = false;
    bool _isWon = false;
    int _remainingTime;
    Coroutine _timer;

    public void EndActivity()
    {
        if (!_isRunning)
            return;

        _isRunning = false;

        StopCoroutine(_timer);

        if (_remainingTime > 0)
        {
            _isWon = true;
        }

        _onEnd.Invoke();
    }

    public void StartActivity()
    {
        _isRunning = true;
        _onStart.Invoke();
        _timer = StartCoroutine(UpdateTimer());
    }

    void Awake()
    {
        _remainingTime = _timeLimitSeconds;
    }

    IEnumerator UpdateTimer()
    {
        while (_remainingTime > 0)
        {
            yield return new WaitForSeconds(1);
            
            _remainingTime--;
        }

        EndActivity();
    }
}
