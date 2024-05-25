using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToSide : MonoBehaviour
{
    [SerializeField]
    Transform _target;

    [SerializeField]
    Vector3 _offset;

    void FixedUpdate()
    {
        transform.position = _target.position + Vector3.up * _offset.y
            + Vector3.ProjectOnPlane(_target.right, Vector3.up).normalized * _offset.x
            + Vector3.ProjectOnPlane(_target.forward, Vector3.up).normalized * _offset.z;
    }
}
