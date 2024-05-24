using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    GameObject _task1Checkmark;
    [SerializeField]
    GameObject _task2Checkmark;
    [SerializeField]
    GameObject _task3Checkmark;

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
    [SerializeField]
    bool _task1Complete;
    [SerializeField]
    bool _task2Complete;
    [SerializeField]
    bool _task3Complete;

    void Awake()
    {
        _task1Checkmark.SetActive(false);
        _task2Checkmark.SetActive(false);
        _task3Checkmark.SetActive(false);

        SetDayNumber(_dayNumber);
        SetEntryText(_entryTextData);
        SetTaskText(1, _task1TextData);
        SetTaskText(2, _task2TextData);
        SetTaskText(3, _task3TextData);
        SetTaskCheckmark(1, _task1Complete);
        SetTaskCheckmark(2, _task2Complete);
        SetTaskCheckmark(3, _task3Complete);
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

    public void SetTaskCheckmark(int taskNumber, bool isComplete)
    {
        switch (taskNumber)
        {
            case 1:
                _task1Checkmark.SetActive(isComplete);
                break;
            case 2:
                _task2Checkmark.SetActive(isComplete);
                break;
            case 3:
                _task3Checkmark.SetActive(isComplete);
                break;
            default:
                Debug.LogError("Task number must be between 1 and 3.");
                break;
        }
    }
}
