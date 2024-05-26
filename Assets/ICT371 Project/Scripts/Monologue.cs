using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monologue : MonoBehaviour
{
    public AudioClip _day1Start;
    public AudioClip _day1End;
    public AudioClip _day2Start;
    public AudioClip _day2End;
    public AudioClip _day3Start;
    public AudioClip _day3End;

    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void playStartDay()
    {
        StartCoroutine(WaitAndPlayStart());
    }

    public void playEndDay()
    {
        StartCoroutine(WaitAndPlayEnd());
    }

    IEnumerator WaitAndPlayStart()
    {
        yield return new WaitForSeconds(3.0f);

        int day = NarrativeSystem.Instance.GetCurrentDay();
        
        switch (day)
        {
            case 1:
                _audioSource.PlayOneShot(_day1Start);
                break;
            case 2:
                _audioSource.PlayOneShot(_day2Start);
                break;
            case 3:
                _audioSource.PlayOneShot(_day3Start);
                break;
        }
    }

    IEnumerator WaitAndPlayEnd()
    {
        yield return new WaitForSeconds(3.0f);

        int day = NarrativeSystem.Instance.GetCurrentDay();
        
        switch (day)
        {
            case 1:
                _audioSource.PlayOneShot(_day1End);
                break;
            case 2:
                _audioSource.PlayOneShot(_day2End);
                break;
            case 3:
                _audioSource.PlayOneShot(_day3End);
                break;
        }
    }
}
