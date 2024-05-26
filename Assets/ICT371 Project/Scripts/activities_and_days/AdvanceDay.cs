using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceDay : MonoBehaviour
{
    [SerializeField]
    Blackout _blackout;

    public void Advance()
    {
        _blackout.FadeOut();
        StartCoroutine(WaitAndAdvance());
    }

    IEnumerator WaitAndAdvance()
    {
        yield return new WaitForSeconds(5.0f);
        NarrativeSystem.Instance.StartNextDay();
        _blackout.FadeIn();
    }
}
