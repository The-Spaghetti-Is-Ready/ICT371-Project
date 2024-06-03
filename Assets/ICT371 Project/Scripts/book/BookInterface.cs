using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <Author>
/// Lane O'Rafferty
/// </Author>
/// <Summary>
/// This class is responsible for controlling the book's (diary's) displayed information.
/// </Summary>
public class BookInterface : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    TextMeshProUGUI _dayNumberText;
    [SerializeField]
    TextMeshProUGUI _entryText;
    [SerializeField]
    TextMeshProUGUI _task1Text;
    [SerializeField]
    TextMeshProUGUI _task2Text;
    [SerializeField]
    TextMeshProUGUI _task3Text;
    [SerializeField]
    RawImage _task1Checkmark;
    [SerializeField]
    RawImage _task2Checkmark;
    [SerializeField]
    RawImage _task3Checkmark;

    [Header("Images")]
    [SerializeField]
    Texture _tickImage;
    [SerializeField]
    Texture _crossImage;

    [Header("Styles")]
    [SerializeField]
    TMP_FontAsset _goodWriting;
    [SerializeField]
    TMP_FontAsset _badWriting;
    [SerializeField]
    TMP_FontAsset _reallyBadWriting;

    [Header("Initial Data")]
    [SerializeField]
    int _dayNumber;
    [SerializeField]
    string _entryTextData;
    [SerializeField]
    string _task1TextData;
    [SerializeField]
    string _task2TextData;
    [SerializeField]
    string _task3TextData;

    /// <summary>
    /// Sets the initial data for the book.
    /// </summary>
    void Awake()
    {
        SetDayNumber(_dayNumber);
        SetEntryText(_entryTextData);
        SetTaskText(1, _task1TextData);
        SetTaskText(2, _task2TextData);
        SetTaskText(3, _task3TextData);
    }

    /// <summary>
    /// Sets the day number displayed on the book.
    /// </summary>
    /// <param name="dayNumber">The day number to set.</param>
    public void SetDayNumber(int dayNumber)
    {
        _dayNumberText.text = dayNumber.ToString();
    }

    /// <summary>
    /// Sets the entry text displayed on the book.
    /// </summary>
    /// <param name="entryText">The entry text to set.</param>
    public void SetEntryText(string entryText)
    {
        _entryText.text = entryText;
    }

    /// <summary>
    /// Sets the task text displayed on the book.
    /// </summary>
    /// <param name="taskNumber">The task number to set.</param>
    /// <param name="taskText">The task text to set.</param>
    public void SetTaskText(int taskNumber, string taskText)
    {
        switch (taskNumber)
        {
            case 1:
                _task1Text.text = taskText;
                break;
            case 2:
                _task2Text.text = taskText;
                break;
            case 3:
                _task3Text.text = taskText;
                break;
            default:
                Debug.LogError("Task number must be between 1 and 3.");
                break;
        }
    }

    /// <summary>
    /// Sets the task completion status and displays the appropriate checkmark or cross image.
    /// </summary>
    /// <param name="taskNumber">The task number to set.</param>
    /// <param name="isWon">The completion status of the task.</param>
    public void SetTaskCompletion(int taskNumber, bool isWon)
    {
        switch (taskNumber)
        {
            case 1:
                _task1Checkmark.gameObject.SetActive(true);
                _task1Checkmark.texture = isWon ? _tickImage : _crossImage;
                break;
            case 2:
                _task2Checkmark.gameObject.SetActive(true);
                _task2Checkmark.texture = isWon ? _tickImage : _crossImage;
                break;
            case 3:
                _task3Checkmark.gameObject.SetActive(true);
                _task3Checkmark.texture = isWon ? _tickImage : _crossImage;
                break;
            default:
                Debug.LogError("Task number must be between 1 and 3.");
                break;
        }
    }

    /// <summary>
    /// Resets the task text and completion status.
    /// </summary>
    public void ResetTasks()
    {
        _task1Text.text = "";
        _task2Text.text = "";
        _task3Text.text = "";
        _task1Checkmark.gameObject.SetActive(false);
        _task2Checkmark.gameObject.SetActive(false);
        _task3Checkmark.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the writing style of the book based on the specified level.
    /// </summary>
    /// <param name="level">The writing level to set.</param>
    public void SetWriting(int level)
    {
        switch (level)
        {
            case 1:
                _entryText.font = _goodWriting;
                _task1Text.font = _goodWriting;
                _task2Text.font = _goodWriting;
                _task3Text.font = _goodWriting;
                break;
            case 2:
                _entryText.font = _badWriting;
                _task1Text.font = _badWriting;
                _task2Text.font = _badWriting;
                _task3Text.font = _badWriting;
                break;
            case 3:
                _entryText.font = _reallyBadWriting;
                _task1Text.font = _reallyBadWriting;
                _task2Text.font = _reallyBadWriting;
                _task3Text.font = _reallyBadWriting;
                break;
            default:
                Debug.LogError("Writing level must be between 1 and 3.");
                break;
        }
    }

    /// <summary>
    /// Updates the book with the current day's data.
    /// </summary>
    public void UpdateBook()
    {
        ResetTasks();
        SetDayNumber(NarrativeSystem.Instance.CurrentDayNumber);
        SetEntryText(NarrativeSystem.Instance.CurrentDay.diaryEntry);

        var activities = NarrativeSystem.Instance.CurrentDay.ActivityList;

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
            SetTaskText(currentIndex + 1, activity.ActivityName);

            // bind activity completion to book interface
            activity.OnEnd.AddListener(() =>
            {
                SetTaskCompletion(currentIndex + 1, activity.IsWon);
            });
        }
    }
}
