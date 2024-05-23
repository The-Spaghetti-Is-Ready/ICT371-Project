using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockSign : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _timeText;
    [SerializeField]
    TextMeshProUGUI _isWonText;
    [SerializeField]
    SeekAndActivate _activity;

    // Start is called before the first frame update
    void Start()
    {
        _activity.StartActivity();
    }

    // Update is called once per frame
    void Update()
    {
        _timeText.text = _activity.RemainingTime.ToString();
        _isWonText.text = _activity.IsWon ? "Yes" : "No";
    }
}
