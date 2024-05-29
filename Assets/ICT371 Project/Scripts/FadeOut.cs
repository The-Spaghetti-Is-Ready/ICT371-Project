using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField]
    Blackout _blackout;

    public void EndGame()
    {
        // _blackout.FadeOut();
        StartCoroutine(WaitAndAdvance());
    }

    IEnumerator WaitAndAdvance()
    {
        yield return new WaitForSeconds(5.0f);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
