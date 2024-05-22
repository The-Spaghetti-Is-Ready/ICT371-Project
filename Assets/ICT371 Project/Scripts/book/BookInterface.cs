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
    public BookOpenClose openClose;
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

    [Header("Field data")]
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

    void Start()
    {
        _task1Checkmark.SetActive(false);
        _task2Checkmark.SetActive(false);
        _task3Checkmark.SetActive(false);

        SetDayNumber(_dayNumber);
        SetEntryText(_entryTextData);
        SetTask1Text(_task1TextData);
        SetTask2Text(_task2TextData);
        SetTask3Text(_task3TextData);
        SetTask1Checkmark(_task1Complete);
        SetTask2Checkmark(_task2Complete);
        SetTask3Checkmark(_task3Complete);
    }

    public void SetDayNumber(int dayNumber)
    {
        _dayNumberText.text = dayNumber.ToString();
    }

    public void SetEntryText(string entryText)
    {
        _entryText.text = entryText;
    }

    public void SetTask1Text(string task1Text)
    {
        _task1Text.text = task1Text;
    }

    public void SetTask2Text(string task2Text)
    {
        _task2Text.text = task2Text;
    }

    public void SetTask3Text(string task3Text)
    {
        _task3Text.text = task3Text;
    }

    public void SetTask1Checkmark(bool isComplete)
    {
        _task1Checkmark.SetActive(isComplete);
    }

    public void SetTask2Checkmark(bool isComplete)
    {
        _task2Checkmark.SetActive(isComplete);
    }

    public void SetTask3Checkmark(bool isComplete)
    {
        _task3Checkmark.SetActive(isComplete);
    }
}
