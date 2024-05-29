using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveBook : MonoBehaviour
{
    [SerializeField]
    GameObject _socket;
    [SerializeField]
    Transform _bookTransform;
    [SerializeField]
    Transform _targetTransform;

    public void Move()
    {
        _socket.SetActive(false);
        _bookTransform.position = _targetTransform.position;
        _socket.SetActive(true);
    }
}
