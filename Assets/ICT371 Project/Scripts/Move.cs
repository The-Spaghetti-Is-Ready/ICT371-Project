using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    Transform _playerTransform;
    [SerializeField]
    Transform _targetTransform;

    public void MovePlayer()
    {
        _playerTransform.position = _targetTransform.position;
    }
}
