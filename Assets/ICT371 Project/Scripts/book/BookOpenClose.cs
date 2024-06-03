using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Author>
/// Lane O'Rafferty
/// </Author>
/// <summary>
/// This class controls the opening and closing of the book.
/// </summary>
public class BookOpenClose : MonoBehaviour
{
    /// <summary>
    /// The pivot point of the book's front cover for opening and closing.
    /// </summary>
    [SerializeField]
    Transform _pivotPoint;

    // The angle of the book's front cover.
    float _angle = 0;
    // The open state of the book.
    bool _isOpen = false;
    // Flags for opening the book.
    bool _isOpening = false;
    // Flags for closing the book.
    bool _isClosing = false;

    // Property for the open state of the book.
    public bool IsOpen { get { return _isOpen; } }
    
    /// <summary>
    /// Toggles the open/close state of the book.
    /// </summary>
    public void ToggleOpenClose()
    {
        if (_isOpening || _isClosing)
            return;

        if (_isOpen)
            StartCoroutine(CloseBook());
        else
            StartCoroutine(OpenBook());
    }

    /// <summary>
    /// Starts opening the book.
    /// </summary>
    public void StartOpen()
    {
        if (_isOpening || _isClosing || _isOpen)
            return;

        StartCoroutine(OpenBook());
    }

    /// <summary>
    /// Starts closing the book.
    /// </summary>
    public void StartClose()
    {
        if (_isOpening || _isClosing || !_isOpen)
            return;

        StartCoroutine(CloseBook());
    }
 
    /// <summary>
    /// Coroutine for opening the book.
    /// </summary>
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

    /// <summary>
    /// Coroutine for closing the book.
    /// </summary>
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
