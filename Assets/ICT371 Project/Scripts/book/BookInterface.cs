using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    void Awake()
    {
        SetDayNumber(_dayNumber);
        SetEntryText(_entryTextData);
        SetTaskText(1, _task1TextData);
        SetTaskText(2, _task2TextData);
        SetTaskText(3, _task3TextData);
    }

    public void SetDayNumber(int dayNumber)
    {
        _dayNumberText.text = dayNumber.ToString();
    }

    public void SetEntryText(string entryText)
    {
        _entryText.text = entryText;
    }

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

    public void ResetTasks()
    {
        _task1Text.text = "";
        _task2Text.text = "";
        _task3Text.text = "";
        _task1Checkmark.gameObject.SetActive(false);
        _task2Checkmark.gameObject.SetActive(false);
        _task3Checkmark.gameObject.SetActive(false);
    }

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
