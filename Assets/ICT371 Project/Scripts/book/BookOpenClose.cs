using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookOpenClose : MonoBehaviour
{
    [SerializeField]
    Transform _pivotPoint;

    float _angle = 0;
    bool _isOpen = false;
    bool _isOpening = false;
    bool _isClosing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleOpenClose()
    {
        if (_isOpening || _isClosing)
            return;

        if (_isOpen)
            StartCoroutine(CloseBook());
        else
            StartCoroutine(OpenBook());
    }

    IEnumerator OpenBook()
    {
        _isOpening = true;

        while (_angle < 180.0f)
        {
            _angle += 180 * Time.deltaTime;
            _pivotPoint.localRotation = Quaternion.Euler(-_angle, 0, 0);
            yield return new WaitForFixedUpdate();
        }

        _angle = 180.0f;
        _pivotPoint.localRotation = Quaternion.Euler(_angle, 0, 0);
        _isOpening = false;
        _isOpen = true;
    }
    IEnumerator CloseBook()
    {
        _isClosing = true;
        
        while (_angle > 0.0f)
        {
            _angle -= 180 * Time.deltaTime;
            _pivotPoint.localRotation = Quaternion.Euler(-_angle, 0, 0);
            yield return new WaitForFixedUpdate();
        }

        _angle = 0.0f;
        _pivotPoint.localRotation = Quaternion.Euler(_angle, 0, 0);
        _isClosing = false;
        _isOpen = false;
    }
}
