using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeekAndActivate : MonoBehaviour, IActivity
{
    public UnityEvent OnStart { get => _onStart; }

    public UnityEvent OnEnd { get => _onEnd;}

    public bool IsWon { get => _isWon; }

    public int RemainingTime { get => _remainingTime; }

    [SerializeField]
    UnityEvent _onStart;
    [SerializeField]
    UnityEvent _onEnd;
    [SerializeField]
    int _timeLimitSeconds = 60;
    [SerializeField]
    ActivationTarget _target;

    int _remainingTime;
    bool _isWon = false;

    public void EndActivity()
    {
        _onEnd.Invoke();
    }

    public void StartActivity()
    {
        _onStart.Invoke();
        StartCoroutine(UpdateTimer());
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
            
            if (_target.IsActivated)
            {
                _isWon = true;
                break;
            }

            _remainingTime--;
        }

        EndActivity();
    }
}
