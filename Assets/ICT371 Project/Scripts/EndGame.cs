using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    float _delay = 5.0f;

    public void End()
    {
        StartCoroutine(WaitAndEnd());
    }

    IEnumerator WaitAndEnd()
    {
        yield return new WaitForSeconds(_delay);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
