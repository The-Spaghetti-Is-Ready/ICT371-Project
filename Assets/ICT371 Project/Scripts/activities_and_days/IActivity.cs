using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <Author>
/// Lane O'Rafferty
/// Marco Garzon Lara
/// </Author>
/// <summary>
/// This interface models an activity in the game.
/// </summary>
public interface IActivity
{
    /// <summary>
    /// Event that is triggered when the activity starts.
    /// </summary>
    UnityEvent OnStart { get; }

    /// <summary>
    /// Event that is triggered when the activity ends.
    /// </summary>
    UnityEvent OnEnd { get; }

    /// <summary>
    /// Gets a value indicating whether the activity is won.
    /// </summary>
    bool IsWon { get; }

    /// <summary>
    /// Gets the name of the activity.
    /// </summary>
    string ActivityName { get; }

    /// <summary>
    /// Starts the activity.
    /// </summary>
    void StartActivity();

    /// <summary>
    /// Ends the activity.
    /// </summary>
    void EndActivity();
}
