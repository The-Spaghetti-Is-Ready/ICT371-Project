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
        _task1Checkmark.gameObject.SetActive(false);
        _task2Checkmark.gameObject.SetActive(false);
        _task3Checkmark.gameObject.SetActive(false);
    }
}
