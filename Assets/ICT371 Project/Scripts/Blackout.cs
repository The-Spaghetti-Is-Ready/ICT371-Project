using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{
    [SerializeField]
    Material _blackoutMaterial;

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            _blackoutMaterial.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        _blackoutMaterial.color = new Color(0, 0, 0, 1);
    }

    IEnumerator FadeInCoroutine()
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            _blackoutMaterial.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        _blackoutMaterial.color = new Color(0, 0, 0, 0);
    }
}
