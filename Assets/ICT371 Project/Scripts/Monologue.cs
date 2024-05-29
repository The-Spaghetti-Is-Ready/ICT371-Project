using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monologue : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> _clips;

    [SerializeField]
    float _delay;

    AudioSource _audioSource;

    int _playIndex = 0;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(int index)
    {
        if (index < 0 || index >= _clips.Count)
        {
            Debug.LogError("Monologue play index out of range");
            return;
        }

        _playIndex = index;
        StartCoroutine(WaitAndPlay());
    }

    IEnumerator WaitAndPlay()
    {
        yield return new WaitForSeconds(_delay);
        _audioSource.PlayOneShot(_clips[_playIndex]);
    }
}
