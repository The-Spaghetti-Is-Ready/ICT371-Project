using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    [SerializeField]
    Image _image;

    float _fadeTime = 2.0f;

    public void FadeOut(float delay)
    {
        if (delay < 0.0f)
        {
            return;
        }

        _fadeTime = delay;
        StartCoroutine(FadeToBlack());
    }

    public void FadeIn(float delay)
    {
        if (delay < 0.0f)
        {
            return;
        }
        
        _fadeTime = delay;
        StartCoroutine(FadeFromBlack());
    }

    IEnumerator FadeToBlack()
    {
        Color color = _image.color;
        for (float t = 0.0f; t < _fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / _fadeTime;
            _image.color = new Color(color.r, color.g, color.b, normalizedTime);
            yield return null;
        }
        _image.color = new Color(color.r, color.g, color.b, 1); // ensure the image is completely black at the end
    }

    IEnumerator FadeFromBlack()
    {
        Color color = _image.color;
        for (float t = 0.0f; t < _fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / _fadeTime;
            _image.color = new Color(color.r, color.g, color.b, 1 - normalizedTime);
            yield return null;
        }
        _image.color = new Color(color.r, color.g, color.b, 0); // ensure the image is completely transparent at the end
    }
}
