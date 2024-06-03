using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;

/// <Author>
/// Lane O'Rafferty
/// </Author>
/// <summary>
/// This class is responsible for invoking events when a collision occurs.
/// </summary>
public class OnCollision : MonoBehaviour
{
    [SerializeField] private string requiredTag;
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;

    /// <summary>
    /// Invokes the onTriggerEnter event when a collider enters the trigger.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (CanInvoke(other.gameObject))
            onTriggerEnter.Invoke();
    }

    /// <summary>
    /// Invokes the onTriggerExit event when a collider exits the trigger.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (CanInvoke(other.gameObject))
            onTriggerExit.Invoke();
    }

    /// <summary>
    /// Determines if the event can be invoked.
    /// </summary>
    private bool CanInvoke(GameObject other)
    {
        return string.IsNullOrEmpty(requiredTag) || other.CompareTag(requiredTag);
    }
}
