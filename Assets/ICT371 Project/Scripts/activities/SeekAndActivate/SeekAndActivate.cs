using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents an activity called "Seek and Activate" that implements the <see cref="IActivity"/> interface.
/// </summary>
public class SeekAndActivate : MonoBehaviour, IActivity
{
    /// <summary>
    /// Event that is triggered when the activity starts.
    /// </summary>
    public UnityEvent OnStart { get => _onStart; }

    /// <summary>
    /// Event that is triggered when the activity ends.
    /// </summary>
    public UnityEvent OnEnd { get => _onEnd;}

    /// <summary>
    /// Indicates whether the activity has been won.
    /// </summary>
    public bool IsWon { get => _isWon; }

    /// <summary>
    /// The name of the activity.
    /// </summary>
    public string ActivityName { get => _activityName; }

    /// <summary>
    /// The remaining time in seconds for the activity.
    /// </summary>
    public int RemainingTime { get => _remainingTime; }

    [SerializeField]
    UnityEvent _onStart;
    [SerializeField]
    UnityEvent _onEnd;
    [SerializeField]
    int _timeLimitSeconds = 60;
    [SerializeField]
    string _activityName = "Seek and Activate";

    /// <summary>
    /// Indicates whether the activity is running.
    /// </summary>
    bool _isRunning = false;
    
    /// <summary>
    /// Indicates whether the activity has been won.
    /// </summary>
    bool _isWon = false;
    
    /// <summary>
    /// The remaining time in seconds for the activity.
    /// </summary>
    int _remainingTime;
    
    /// <summary>
    /// The timer coroutine.
    /// </summary>
    Coroutine _timer;

    /// <summary>
    /// Ends the activity.
    /// </summary>
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

    /// <summary>
    /// Starts the activity.
    /// </summary>
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

    /// <summary>
    /// Updates the timer by decreasing the remaining time by 1 every second until it reaches 0.
    /// </summary>
    /// <returns>An IEnumerator used for coroutine execution.</returns>
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
